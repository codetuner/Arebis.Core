using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.Core.Localization
{
    /// <summary>
    /// Represents a localization data source service.
    /// </summary>
    public interface ILocalizationSource
    {
        /// <summary>
        /// Retrieves a LocalizationResourceSet from the source.
        /// </summary>
        LocalizationResourceSet GetResourceSet();
    }
}
