using Arebis.Core.Localization;
using Microsoft.AspNetCore.Http;
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
    /// An IStringLocalizerFactory implementation returning source localized Localizers.
    /// To be registered as Singleton service for type <see cref="IStringLocalizerFactory"/>.
    /// </summary>
    public class StringLocalizerFactory : IStringLocalizerFactory
    {
        /// <summary>
        /// Constructs an instance of StringLocalizerFactory.
        /// </summary>
        public StringLocalizerFactory(IOptions<LocalizationOptions> localizationOptions, ILocalizationResourceProvider resourceProvider, IHttpContextAccessor? httpContextAccessor, ILogger<Localizer> logger)
        {
            this.localizer = new Localizer(localizationOptions, resourceProvider, httpContextAccessor, logger);
        }

        private readonly Localizer localizer;

        /// <inheritdoc/>
        public IStringLocalizer Create(Type resourceSource)
        {
            return localizer;
        }

        /// <inheritdoc/>
        public IStringLocalizer Create(string baseName, string location)
        {
            return localizer;
        }
    }
}
