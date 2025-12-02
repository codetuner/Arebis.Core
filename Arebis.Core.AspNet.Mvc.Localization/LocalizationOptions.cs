using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.Core.AspNet.Mvc.Localization
{
    /// <summary>
    /// Options of the Arebis Core Asp.NET localization component.
    /// </summary>
    public class LocalizationOptions
    {
        /// <summary>
        /// If set, path to which compiled localization data is stored and fetched.
        /// </summary>
        public string? CacheFileName { get; set; }

        /// <summary>
        /// Whether to allow setting the "__LocalizeFormat" query string parameter to overwrite default localization rendering.
        /// Meant for development only.
        /// </summary>
        public bool AllowLocalizeFormat { get; set; }

        /// <summary>
        /// Domains of localization.
        /// </summary>
        public string[] Domains { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Whether to use only reviewed localization key values.
        /// If false, non-reviewed keys are also used.
        /// Defaults to true.
        /// </summary>
        public bool UseOnlyReviewedLocalizationValues { get; set; } = true;

        /// <summary>
        /// The key that contains the culture name.
        /// Defaults to "culture".
        /// </summary>
        public string? RouteDataStringKey { get; set; }

        /// <summary>
        /// The key that contains the UI culture name.
        /// If not specified or no value is found, RouteDataStringKey will be used.
        /// Defaults to "ui-culture".
        /// </summary>
        public string? UIRouteDataStringKey { get; set; }
    }
}
