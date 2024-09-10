using Arebis.Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arebis.Core.Services.Translation
{
    /// <summary>
    /// An ITranslationService implementation using Microsoft Bing Translation API.
    /// Following configuration keys are required: "BingApi:SubscriptionKey" (i.e. "0123456789abcdef0123456789abcdef")
    /// and "BingApi:Region" (Azure region, i.e. "centralus", see https://docs.microsoft.com/en-us/azure/media-services/latest/azure-regions-code-names).
    /// </summary>
    public class BingTranslationService : ITranslationService, IDisposable
    {
        private const int MaxBatchSize = 1000;

        private HttpClient? httpClient = null;
        private IConfigurationSection configSection;
        private readonly ILogger logger;
        private readonly IHttpClientFactory? httpClientFactory;

        /// <summary>
        /// Constructs a <see cref="BingTranslationService"/>.
        /// </summary>
        public BingTranslationService(IConfiguration configuration, ILogger<BingTranslationService> logger, IHttpClientFactory? httpClientFactory = null)
        {
            this.configSection = configuration.GetSection("BingApi");
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string?>> TranslateAsync(string fromLanguage, string toLanguage, string mimeType, IEnumerable<string> sources, CancellationToken ct = default)
        {
            var result = new List<string?>();
            var responseObjects = await TranslateInternalAsync(fromLanguage, new string[] { toLanguage }, mimeType, sources, ct);
            foreach (var item in responseObjects)
            {
                if (item.Translations?[0]?.TranslatedText != null) result.Add(item.Translations[0].TranslatedText);
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string?>> TranslateAsync(string fromLanguage, IEnumerable<string> toLanguages, string mimeType, string source, CancellationToken ct = default)
        {
            var result = new List<string?>();
            var responseObjects = await TranslateInternalAsync(fromLanguage, toLanguages, mimeType, new string[] { source }, ct);
            foreach (var textitem in responseObjects?[0]?.Translations ?? Array.Empty<TranslateResponseItem.TranslationDataItem>())
            {
                result.Add(textitem.TranslatedText);
            }

            return result;
        }

        /// <summary>
        /// Internal translation implementation.
        /// </summary>
        protected virtual async Task<List<TranslateResponseItem>> TranslateInternalAsync(string fromLanguage, IEnumerable<string> toLanguages, string mimeType, IEnumerable<string> sources, CancellationToken ct = default)
        {
            var result = new List<TranslateResponseItem>();
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
                    var requestObject = new List<TranslateRequestItem>();

                    foreach (var text in sourcesBatch)
                    {
                        requestObject.Add(new TranslateRequestItem { Text = text });
                    }

                    ct.ThrowIfCancellationRequested();

                    this.httpClient ??= BuildHttpClient();
                    var textType = (mimeType == MediaTypeNames.Text.Plain) ? "plain" : (mimeType == MediaTypeNames.Text.Html) ? "html" : null;
                    var url = (configSection["TranslationServiceUrl"] ?? "https://api.cognitive.microsofttranslator.com/translate") + $"?api-version=3.0&from={fromLanguage}&to={String.Join("&to=", toLanguages)}&textType={textType}";
                    using (var response = await this.httpClient.PostAsJsonAsync(url, requestObject, ct))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var responseObjects = (TranslateResponseItem[]?)JsonSerializer.Deserialize<TranslateResponseItem[]>(responseContent);
                            if (responseObjects != null) result.AddRange(responseObjects);
                        }
                        else
                        {
                            var ex = new InvalidOperationException($"BingTranslateService call failed: {response.ReasonPhrase}.");
                            ex.Data["StatusCodeName"] = response.StatusCode;
                            ex.Data["StatusCode"] = (int)response.StatusCode;
                            ex.Data["StatusMessage"] = response.ReasonPhrase;
                            ex.Data["Content"] = await response.Content.ReadAsStringAsync();
                            ex.Data["Arg.fromLanguage"] = fromLanguage;
                            ex.Data["Arg.toLanguages"] = String.Join(", ", toLanguages);
                            ex.Data["Arg.mimeType"] = mimeType;
                            logger.LogError(ex, "Failed to translate using BingTranslationService.");
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
        /// Returns an HttpClient to perform API calls to Bing.
        /// </summary>
        protected virtual HttpClient BuildHttpClient()
        {
            var httpClient = this.httpClientFactory?.CreateClient() ?? new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", configSection["SubscriptionKey"]);
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Region", configSection["Region"]);
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
        /// A Bing translation request.
        /// </summary>
        public class TranslateRequestItem
        {
            /// <summary>
            /// The text to translate.
            /// </summary>
            [JsonPropertyName("Text")]
            public string? Text { get; set; }
        }

        /// <summary>
        /// A Bing translation response item.
        /// </summary>
        public class TranslateResponseItem
        {
            /// <summary>
            /// A Bing translation response data item.
            /// </summary>
            [JsonPropertyName("translations")]
            public TranslationDataItem[]? Translations { get; set; }

            /// <summary>
            /// A Bing translation response data item.
            /// </summary>
            public class TranslationDataItem
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
