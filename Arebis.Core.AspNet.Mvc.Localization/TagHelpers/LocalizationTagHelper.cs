﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Arebis.Core.AspNet.Mvc.Localization.TagHelpers
{
    /// <summary>
    /// The LocalizationTagHelper defines "localization-key" attribute to hold a localization resource key.
    /// If a resource with this key is found, the content of this tag is replaced by the localization value.
    /// </summary>
    /// <example>
    /// &lt;div localization-key="(Setup instructions for {0})" localization-arg-0="@(name)"&gt;
    /// </example>
    [HtmlTargetElement(Attributes = "localization-key")]
    public class LocalizationTagHelper : TagHelper
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHtmlLocalizerFactory htmlLocalizerFactory;
        private readonly ILogger<LocalizationTagHelper> logger;

        /// <summary>
        /// Constructs a LocalizationTagHelper.
        /// </summary>
        public LocalizationTagHelper(IWebHostEnvironment webHostEnvironment, IHtmlLocalizerFactory htmlLocalizerFactory, ILogger<LocalizationTagHelper> logger)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.htmlLocalizerFactory = htmlLocalizerFactory;
            this.logger = logger;
        }

        /// <summary>
        /// Gets or sets the view context (automatically set when using razor views).
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext? ViewContext { get; set; }

        /// <summary>
        /// The key of localization resouce to replace the content of this tag with.
        /// </summary>
        [HtmlAttributeName("localization-key")]
        public string? LocalizationKey { get; set; }

        /// <summary>
        /// Additional numbered arguments.
        /// </summary>
        [HtmlAttributeName("localization-args", DictionaryAttributePrefix = "localization-arg-")]
        public IDictionary<string, string> Arguments { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Processes this tag helper.
        /// </summary>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Check required parameters:
            if (this.LocalizationKey == null)
            {
                base.Process(context, output);
                return;
            }
            else if (this.ViewContext == null)
            {
                logger.LogWarning("No ViewContext injected on LocalizationTagHelper; ");
                base.Process(context, output);
                return;
            }

            try
            {
                // Get localizer:
                var page = (this.ViewContext.View as Microsoft.AspNetCore.Mvc.Razor.RazorView)?.RazorPage;
                var baseName = (page != null)
                    ? page.GetType().Assembly.GetName().Name + page.Path.Substring(0, page.Path.IndexOf('.')).Replace('/', '.')
                    : Assembly.GetEntryAssembly()!.GetName().Name + this.ViewContext.ExecutingFilePath?.Substring(0, this.ViewContext.ExecutingFilePath.IndexOf('.')).Replace('/', '.');
                var localizer = this.htmlLocalizerFactory.Create(baseName, this.webHostEnvironment.ApplicationName);

                // Retrieve localized value:
                var arguments = this.Arguments.OrderBy(p => Int32.Parse(p.Key)).Select(p => (object)p.Value).ToArray();
                var value = localizer[LocalizationKey, arguments];

                // Render value, or tag content if not found:
                if (value.IsResourceNotFound)
                {
                    base.Process(context, output);
                }
                else
                {
                    output.Content.SetHtmlContent(value);
                }
            }
            catch (Exception ex)
            {
                ex = new InvalidOperationException("Error in localization tag, see inner exception and data for more info.", ex);
                ex.Data["ViewContext.ExecutingFilePath"] = this.ViewContext.ExecutingFilePath;
                ex.Data["LocalizationKey"] = this.LocalizationKey;
                throw ex;
            }
        }
    }
}
