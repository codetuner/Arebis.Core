using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Localization
{
    /// <summary>
    /// Defines a localization resource provider service.
    /// </summary>
    public interface ILocalizationResourceProvider
    {
        /// <summary>
        /// Returns the default culture name.
        /// </summary>
        string GetDefaultCulture();

        /// <summary>
        /// Returns the list of supported culture names.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetCultures();

        /// <summary>
        /// Retrieves a resource for the given key and path.
        /// </summary>
        LocalizationResource? GetResource(string key, string? forPath);

        /// <summary>
        /// Refreshes resources to be up to date.
        /// </summary>
        void Refresh();
    }
}
