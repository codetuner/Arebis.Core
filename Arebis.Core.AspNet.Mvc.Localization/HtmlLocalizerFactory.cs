using Arebis.Core.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.AspNet.Mvc.Localization
{
    /// <summary>
    /// An IHtmlLocalizerFactory implementation returning source localized Localizers.
    /// To be registered as Singleton service for type <see cref="IHtmlLocalizerFactory"/>.
    /// </summary>
    public class HtmlLocalizerFactory : IHtmlLocalizerFactory
    {
        /// <summary>
        /// Constructs an instance of HtmlLocalizerFactory.
        /// </summary>
        public HtmlLocalizerFactory(IOptions<LocalizationOptions> localizationOptions, ILocalizationResourceProvider resourceProvider, IHttpContextAccessor? httpContextAccessor, ILogger<Localizer> logger)
        {
            this.localizer = new Localizer(localizationOptions, resourceProvider, httpContextAccessor, logger);
        }

        private readonly Localizer localizer;

        /// <inheritdoc/>
        public IHtmlLocalizer Create(Type resourceSource)
        {
            return localizer;
        }

        /// <inheritdoc/>
        public IHtmlLocalizer Create(string baseName, string location)
        {
            return localizer;
        }
    }
}
