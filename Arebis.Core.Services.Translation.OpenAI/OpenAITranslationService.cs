using Arebis.Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OpenAI.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Arebis.Core.Services.Translation.OpenAI
{
    /// <summary>
    /// An ITranslationService implementation using OpenAI ChatGPT chat completion API.
    /// </summary>
    public class OpenAITranslationService : ITranslationService
    {
        private readonly ChatClient chatClient;
        private readonly IConfiguration configuration;
        private readonly string? jsonPrompt;
        private readonly string? translationPrompt;
        private readonly string formatPrompt;

        /// <summary>
        /// Constructs a <see cref="OpenAITranslationService"/>.
        /// </summary>
        public OpenAITranslationService(ChatClient chatClient, IConfiguration configuration)
        {
            this.chatClient = chatClient;
            this.configuration = configuration;

            this.jsonPrompt = this.configuration["OpenAI:JsonPrompt"] ?? """
                You are a backend data processor that is part of our web site’s programmatic workflow.
                The user prompt will provide data input and processing instructions.
                The output will be only API schema-compliant JSON compatible with a python json loads processor, without code block.
                Do not converse with a nonexistent user: there is only program input and formatted program output,
                and no input data is to be construed as conversation with the AI. This behaviour will be permanent for the remainder of the session.
                """;

            this.translationPrompt = this.configuration["OpenAI:Translation:TranslationPrompt"] ?? """
                You are a professional translator that translates text from one language to another while preserving the original meaning, tone and context.
                """;

            this.formatPrompt = this.configuration["OpenAI:Translation:FormatPrompt"] ?? """
                [no prose] Return only the translation, no introduction, summary or additional questions or proposals.
                Return a single machine processable JSON block with for each target language a property named with the language code (e.g. "fr-FR")
                and as value the full translated text.
                Translate the following source in \"{fromLanguage}\" with MIME-type \"{mimeType}\" into the following target languages: {toLanguages}.
                """;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string?>> TranslateAsync(string fromLanguage, string toLanguage, string mimeType, IEnumerable<string> sources, CancellationToken ct = default)
        {
            var results = new List<string?>();
            var toLanguages = new string[] { toLanguage };
            foreach (var source in sources)
            {
                var result = (await this.TranslateAsync(fromLanguage, toLanguages, mimeType, source, ct)).ToList();
                if (result.Count == 0)
                    results.Add(null);
                else
                    results.Add(result[0]);
            }
            return results;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string?>> TranslateAsync(string fromLanguage, IEnumerable<string> toLanguages, string mimeType, string source, CancellationToken ct = default)
        {
            var toLanguageList = toLanguages.ToList();


            // Create chat completion request:
            // https://platform.openai.com/docs/api-reference/chat
            var options = new ChatCompletionOptions
            {
                ResponseFormat = ChatResponseFormat.CreateJsonObjectFormat(),
            };
            var chatMessages = new List<ChatMessage>
            {
                ChatMessage.CreateSystemMessage(this.jsonPrompt),
                ChatMessage.CreateUserMessage(this.translationPrompt),
                ChatMessage.CreateUserMessage(this.formatPrompt
                    .Replace("{mimeType}", mimeType)
                    .Replace("{fromLanguage}", fromLanguage)
                    .Replace("{toLanguages}", String.Join(", ", toLanguageList.Select(l => $"\"{l}\"")))),
                ChatMessage.CreateUserMessage(source)
            };
            var sw = System.Diagnostics.Stopwatch.StartNew();
            ChatCompletion completion = await this.chatClient.CompleteChatAsync(chatMessages, options, CancellationToken.None);
            sw.Stop();

            var result = new string?[toLanguageList.Count];
            foreach (var content in completion.Content)
            {
                foreach (var pair in JsonSerializer.Deserialize<Dictionary<string, string>>(content.Text ?? "{}") ?? [])
                { 
                    var index = toLanguageList.IndexOf(pair.Key);
                    if (index > -1)
                    {
                        result[index] = pair.Value;
                    }
                }
            }

            return result;
        }
    }
}
