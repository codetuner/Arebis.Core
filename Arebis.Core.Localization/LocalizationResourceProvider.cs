using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace Arebis.Core.Localization
{
    /// <summary>
    /// Provides access to localization resources loaded form a localization source.
    /// To be registered as Singleton service for type <see cref="ILocalizationResourceProvider"/>.
    /// </summary>
    public class LocalizationResourceProvider : ILocalizationResourceProvider
    {
        private static readonly Object SyncObject = new();

        private readonly ILocalizationSource? localizationSource;
        private readonly ILocalizationResourcePersistor? localizationResourcePersistor;
        private readonly ILogger<LocalizationResourceProvider> logger;

        private volatile LocalizationResourceSet? resourceSet;

        /// <summary>
        /// Constructs a LocalizationResourceProvider.
        /// </summary>
        public LocalizationResourceProvider(ILogger<LocalizationResourceProvider> logger, ILocalizationSource? localizationSource = null, ILocalizationResourcePersistor? localizationResourcePersistor = null)
        {
            this.localizationSource = localizationSource;
            this.localizationResourcePersistor = localizationResourcePersistor;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public string GetDefaultCulture()
        {
            LazyLoadResources();

            return this.resourceSet.DefaultCulture;
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetCultures()
        {
            LazyLoadResources();

            return this.resourceSet.Cultures;
        }

        /// <inheritdoc/>
        public LocalizationResource? GetResource(string key, string? forPath)
        {
            LazyLoadResources();

            if (this.resourceSet.Resources.TryGetValue(key, out var candidates))
            {
                // Resource key found, run over candidate resources for matching path:
                if (forPath != null) 
                { 
                    foreach (var candidate in candidates)
                    {
                        if (forPath.StartsWith(candidate.ForPath ?? String.Empty, StringComparison.OrdinalIgnoreCase))
                        {
                            // If found a match, return the candidate resource:
                            return candidate;
                        }
                    }
                }
                else
                {
                    return candidates.SingleOrDefault(c => c.ForPath == null || c.ForPath.Length == 0);
                }
            }

            // In all other cases, resource key was not found:
            return null;
        }

        [MemberNotNull(nameof(resourceSet))]
        private void LazyLoadResources()
        {
            // If no resources are loaded in memory yet, load from cache and/or from source:
            if (this.resourceSet == null)
            {
                lock (SyncObject)
                {
                    // If cache file name defined, try loading from cache file:
                    if (this.resourceSet == null && this.localizationResourcePersistor != null)
                    {
                        this.resourceSet = this.localizationResourcePersistor.TryLoad();
                    }
                    // If still not loaded, (re)load from source:
                    if (this.resourceSet == null)
                    {
                        Refresh();
                    }
                }
            }
        }

        /// <summary>
        /// Forces reload of localization resources from the registered ILocalizationSource.
        /// </summary>
        [MemberNotNull(nameof(resourceSet))]
        public void Refresh()
        {
            if (this.localizationSource == null)
            {
                throw new InvalidOperationException("Cannot reload localization from source if no ILocalizationSource service was registered.");
            }

            lock (SyncObject)
            {
                this.resourceSet = this.localizationSource.GetResourceSet();
                logger.LogInformation("Localization resources loaded form source.");

                if (this.localizationResourcePersistor != null)
                {
                    this.localizationResourcePersistor.TrySave(this.resourceSet);
                }
            }
        }
    }
}
