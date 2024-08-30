using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Localization
{
    /// <summary>
    /// Represents a localization data persistor service.
    /// Implementations should persist localization data in such a way that it can quickly be reloaded.
    /// Used to make localization resource data survive restarts without the need to a full
    /// reload of the source.
    /// </summary>
    public interface ILocalizationResourcePersistor
    {
        /// <summary>
        /// Event triggered when a change is detected on the persisted data.
        /// </summary>
        public event EventHandler? OnChange;

        /// <summary>
        /// Tries to load persisted resources set. Returns null if not found or failed.
        /// </summary>
        LocalizationResourceSet? TryLoad();

        /// <summary>
        /// Tries to persist resource set.
        /// </summary>
        void TrySave(LocalizationResourceSet? resourceSet);
    }
}
