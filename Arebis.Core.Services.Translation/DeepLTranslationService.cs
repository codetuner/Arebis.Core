using Arebis.Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arebis.Core.Services.Translation
{
    /// <summary>
    /// An ITranslationService implementation using DeepL.com API.
    /// Following configuration keys are required: "DeepLApi:ServiceUrl" (the service URL to use, i.e. "https://api-free.deepl.com/")
    /// and "DeepLApi:ApiKey" (API key prefixed with "DeepL-Auth-Key ", i.e. "DeepL-Auth-Key 01234567-89ab-cdef-0123-456789abcdef:fx").
    /// </summary>
    public class DeepLTranslationService : ITranslationService, IDisposable
    {
        private const int MaxBatchSize = 50;

        private HttpClient? httpClient = null;
        private readonly IConfigurationSection configSection;
        private readonly ILogger logger;
        private readonly IHttpClientFactory? httpClientFactory;

        /// <summary>
        /// Constructs a <see cref="DeepLTranslationService"/>.
        /// </summary>
        public DeepLTranslationService(IConfiguration configuration, ILogger<DeepLTranslationService> logger, IHttpClientFactory? httpClientFactory = null)
        {
            this.configSection = configuration.GetSection("DeepLApi");
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
            this.Context = configSection["Context"];
            this.PreserveFormatting = bool.Parse(configSection["PreserveFormatting"] ?? "false");
            this.Formality = configSection["Formality"];
            this.ModelType = configSection["ModelType"];
            this.IgnoreTags = configSection["IgnoreTags"];
        }

        /// <summary>
        /// The context to use for the translation. This is a free text that can be used to provide additional information about the translation.
        /// Set the context before calling the Translate method.
        /// </summary>
        public string? Context { get; set; }

        /// <summary>
        /// Sets whether the translation engine should respect the original formatting, even if it would usually correct some aspects.
        /// See <see href="https://developers.deepl.com/docs/api-reference/translate/openapi-spec-for-text-translation"/> for more information.
        /// </summary>
        public bool PreserveFormatting { get; set; }

        /// <summary>
        /// Sets whether the translated text should lean towards formal or informal language.
        /// Possible values: default, more, less, prefer_more, prefer_less.
        /// See <see href="https://developers.deepl.com/docs/api-reference/translate/openapi-spec-for-text-translation"/> for more information.
        /// </summary>
        public string? Formality { get; set; }

        /// <summary>
        /// Specifies which DeepL model should be used for translation.
        /// Possible values: quality_optimizedprefer_quality_optimizedlatency_optimized.
        /// See <see href="https://developers.deepl.com/docs/api-reference/translate/openapi-spec-for-text-translation"/> for more information.
        /// </summary>
        public string? ModelType { get; set; }

        /// <summary>
        /// Config key prefix for glossary keys.
        /// Defaults to "Glossary".
        /// When translating, the system looks for a "{GlossaryKeyPrefix}-{fromLanguage}-{toLanguage}" key in the configuration. If found, it's value
        /// is used as Glossary Id.
        /// </summary>
        public string GlossaryKeyPrefix { get; set; } = "Glossary";

        /// <summary>
        /// Comma-separated list of XML tags that indicate text not to be translated.
        /// See <see href="https://developers.deepl.com/docs/api-reference/translate/openapi-spec-for-text-translation"/> for more information.
        /// </summary>
        public string? IgnoreTags { get; set; }

        /// <inheritdoc/>
        public async Task<IEnumerable<string?>> TranslateAsync(string fromLanguage, IEnumerable<string> toLanguages, string mimeType, string source, CancellationToken ct = default)
        {
            var result = new List<string?>();
            var sources = new string[] { source };
            foreach (var toLanguage in toLanguages)
            {
                try
                {
                    var texts = await this.TranslateAsync(fromLanguage, toLanguage, mimeType, sources, ct);
                    result.Add(texts.FirstOrDefault());
                }
                catch (Exception ex)
                {
                    // Log the error as warning:
                    logger.LogWarning(ex, "Failed to translate using DeepLTranslationService.");

                    // Since DeepL is very strict on language codes, on failure (i.e. non-supported language) add null:
                    result.Add(null);
                }
            }
            return result;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string?>> TranslateAsync(string fromLanguage, string toLanguage, string mimeType, IEnumerable<string> sources, CancellationToken ct = default)
        {
            var result = new List<string?>();
            var sourcesEnumerator = sources.GetEnumerator();
            var glossary = configSection[$"Glossary-{fromLanguage}-{toLanguage}"];
            while (true)
            {
                var sourcesBatch = new List<string>();
                for (int i = 0; i < MaxBatchSize; i++)
                {
                    if (sourcesEnumerator.MoveNext())
                    {
                        sourcesBatch.Add(sourcesEnumerator.Current);
                    }
                    else
                    {
                        break;
                    }
                }

                if (sourcesBatch.Count > 0)
                {
                    var data = BuildData(mimeType);
                    data.Add(new KeyValuePair<string, string>("source_lang", fromLanguage));
                    data.Add(new KeyValuePair<string, string>("target_lang", toLanguage));
                    if (!String.IsNullOrEmpty(Context)) data.Add(new KeyValuePair<string, string>("context", Context));
                    if (PreserveFormatting) data.Add(new KeyValuePair<string, string>("preserve_formatting", "true"));
                    if (!String.IsNullOrEmpty(Formality)) data.Add(new KeyValuePair<string, string>("formality", Formality));
                    if (!String.IsNullOrEmpty(ModelType)) data.Add(new KeyValuePair<string, string>("model_type", ModelType));
                    if (!String.IsNullOrEmpty(glossary)) data.Add(new KeyValuePair<string, string>("glossary_id", glossary));
                    if (!String.IsNullOrEmpty(IgnoreTags)) data.Add(new KeyValuePair<string, string>("ignore_tags", IgnoreTags));
                    foreach (var text in sourcesBatch)
                    {
                        data.Add(new KeyValuePair<string, string>("text", text));
                    }
                    var content = new FormUrlEncodedContent(data);

                    ct.ThrowIfCancellationRequested();

                    this.httpClient ??= BuildHttpClient();
                    using (var response = await this.httpClient.PostAsync(configSection["TranslationServiceUrl"], content, ct))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var responseData = (TranslateResponse?)JsonSerializer.Deserialize<TranslateResponse>(responseContent);
                            foreach (var item in responseData?.Translations ?? Array.Empty<TranslateResponse.Translation>())
                            {
                                if (!String.IsNullOrEmpty(item.TranslatedText)) result.Add(item.TranslatedText);
                            }
                        }
                        else
                        {
                            var ex = new InvalidOperationException("DeepLTranslatorService call failed.");
                            ex.Data["StatusCodeName"] = response.StatusCode;
                            ex.Data["StatusCode"] = (int)response.StatusCode;
                            ex.Data["StatusMessage"] = response.ReasonPhrase;
                            ex.Data["Content"] = await response.Content.ReadAsStringAsync();
                            ex.Data["Arg.fromLanguage"] = fromLanguage;
                            ex.Data["Arg.toLanguage"] = toLanguage;
                            ex.Data["Arg.mimeType"] = mimeType;
                            logger.LogError(ex, "Failed to translate using DeepLTranslationService.");
                            throw ex;
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns an HttpClient to perform API calls to DeepL.
        /// </summary>
        protected virtual HttpClient BuildHttpClient()
        {
            var httpClient = this.httpClientFactory?.CreateClient() ?? new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", configSection["ApiKey"]);
            return httpClient;
        }

        /// <summary>
        /// Builds input data for the DeepL translation service.
        /// </summary>
        protected virtual List<KeyValuePair<string,string>> BuildData(string mimeType)
        {
            var data = new List<KeyValuePair<string, string>>();
            if (mimeType == MediaTypeNames.Text.Xml)
            {
                data.Add(new KeyValuePair<string, string>("tag_handling", "xml"));
            }
            else if (mimeType == MediaTypeNames.Text.Html)
            {
                data.Add(new KeyValuePair<string, string>("tag_handling", "html"));
            }
            return data;
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            if (httpClient != null)
            {
                this.httpClient.Dispose();
                this.httpClient = null;
            }
        }

        /// <summary>
        /// A DeepL translation response.
        /// </summary>
        public class TranslateResponse
        {
            /// <summary>
            /// DeepL translations.
            /// </summary>
            [JsonPropertyName("translations")]
            public Translation[]? Translations { get; set; }

            /// <summary>
            /// A DeepL translation.
            /// </summary>
            public class Translation
            {
                /// <summary>
                /// Translated text.
                /// </summary>
                [JsonPropertyName("text")]
                public string? TranslatedText { get; set; }
            }
        }
    }
}
