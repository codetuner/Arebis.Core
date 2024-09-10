using Arebis.Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arebis.Core.Services.Translation
{
    /// <summary>
    /// An ITranslationService implementation using basic Google Cloud Translation API.
    /// Following configuration key is required: "GoogleApi:ApiKey" (i.e. "AbCdEfG81jKlMn0pQrStU4w-4BcDeFgH12kLmNo").
    /// </summary>
    public class GoogleTranslationService : ITranslationService, IDisposable
    {
        private const int MaxBatchSize = 128;

        private HttpClient? httpClient = null;
        private IConfigurationSection configSection;
        private readonly ILogger logger;
        private readonly IHttpClientFactory? httpClientFactory;

        /// <summary>
        /// Constructs a <see cref="GoogleTranslationService"/>.
        /// </summary>
        public GoogleTranslationService(IConfiguration configuration, ILogger<GoogleTranslationService> logger, IHttpClientFactory? httpClientFactory = null)
        {
            this.configSection = configuration.GetSection("GoogleApi");
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

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
                catch (Exception)
                {
                    // Since Google is strict on translation language pairs, on failure add null:
                    result.Add(null);
                }
            }
            return result;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string?>> TranslateAsync(string fromLanguage, string toLanguage, string mimeType, IEnumerable<string> sources, CancellationToken ct = default)
        {
            var result = new List<string>();
            var sourcesEnumerator = sources.GetEnumerator();
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
                    var requestObject = new TranslateRequest()
                    {
                        SourceLanguage = fromLanguage,
                        TargetLanguage = toLanguage,
                        Format = (mimeType == MediaTypeNames.Text.Plain) ? "text" : (mimeType == MediaTypeNames.Text.Html) ? "html" : null
                    };

                    foreach (var text in sourcesBatch)
                    {
                        requestObject.Texts.Add(text);
                    }

                    ct.ThrowIfCancellationRequested();

                    this.httpClient ??= BuildHttpClient();
                    using (var response = await this.httpClient.PostAsJsonAsync((configSection["TranslationServiceUrl"] ?? "https://translation.googleapis.com/language/translate/v2") + "?key=" + configSection["ApiKey"], requestObject, ct))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var responseObject = (TranslateResponse?)JsonSerializer.Deserialize<TranslateResponse>(responseContent);
                            foreach (var item in responseObject?.Data?.Translations ?? Array.Empty<TranslateResponse.TranslationDataItem>())
                            {
                                if (!String.IsNullOrEmpty(item.TranslatedText)) result.Add(item.TranslatedText);
                            }
                        }
                        else
                        {
                            var ex = new InvalidOperationException("GoogleTranslateService call failed.");
                            ex.Data["StatusCodeName"] = response.StatusCode;
                            ex.Data["StatusCode"] = (int)response.StatusCode;
                            ex.Data["StatusMessage"] = response.ReasonPhrase;
                            ex.Data["Content"] = await response.Content.ReadAsStringAsync();
                            ex.Data["Arg.fromLanguage"] = fromLanguage;
                            ex.Data["Arg.toLanguage"] = toLanguage;
                            ex.Data["Arg.mimeType"] = mimeType;
                            logger.LogError(ex, "Failed to translate using GoogleTranslationService.");
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
        /// Returns an HttpClient to perform API calls to Google.
        /// </summary>
        protected virtual HttpClient BuildHttpClient()
        {
            var httpClient = this.httpClientFactory?.CreateClient() ?? new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("Authorization", configSection["ApiKey"]);
            return httpClient;
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
        /// A Google Translate Request.
        /// </summary>
        public class TranslateRequest
        {
            /// <summary>
            /// The texts to translate.
            /// </summary>
            [JsonPropertyName("q")]
            public List<string> Texts { get; } = new List<string>();

            /// <summary>
            /// Language of the input texts. If null, Google will auto-detect.
            /// </summary>
            /// <seealso href="https://cloud.google.com/translate/docs/languages"/>
            [JsonPropertyName("source")]
            public string? SourceLanguage { get; set; }

            /// <summary>
            /// Language to translate the texts into.
            /// </summary>
            /// <seealso href="https://cloud.google.com/translate/docs/languages"/>
            [JsonPropertyName("target")]
            public string? TargetLanguage { get; set; }

            /// <summary>
            /// Input format: "text" or "html".
            /// </summary>
            [JsonPropertyName("format")]
            public string? Format { get; set; }
        }

        /// <summary>
        /// A Google Translate Response.
        /// </summary>
        public class TranslateResponse
        {
            /// <summary>
            /// Translation data.
            /// </summary>
            [JsonPropertyName("data")]
            public TranslationData? Data { get; set; }

            /// <summary>
            /// Translation data.
            /// </summary>
            public class TranslationData
            {
                /// <summary>
                /// Translation data item.
                /// </summary>
                [JsonPropertyName("translations")]
                public TranslationDataItem[]? Translations { get; set; }
            }

            /// <summary>
            /// Translation data item.
            /// </summary>
            public class TranslationDataItem
            {
                /// <summary>
                /// Translated text.
                /// </summary>
                [JsonPropertyName("translatedText")]
                public string? TranslatedText { get; set; }

                /// <summary>
                /// Detected source language.
                /// </summary>
                [JsonPropertyName("detectedSourceLanguage")]
                public string? DetectedSourceLanguage { get; set; }
            }
        }
    }
}
