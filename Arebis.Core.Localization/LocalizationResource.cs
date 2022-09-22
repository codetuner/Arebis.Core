using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.Core.Localization
{
    /// <summary>
    /// A localization resource with the values in various cultures.
    /// </summary>
    [Serializable]
    public class LocalizationResource
    {
        /// <summary>
        /// Path for which this resource applies.
        /// </summary>
        public string? ForPath { get; init; }

        /// <summary>
        /// Fields to be substituted with data or other values.
        /// </summary>
        public HashSet<string>? SubstitutionFields { get; set; }

        /// <summary>
        /// Resource values per culture name.
        /// </summary>
        public Dictionary<string, string> Values { get; init; } = new Dictionary<string, string>();
    }
}
