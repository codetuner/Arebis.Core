using Arebis.Core.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Runtime.Serialization.Formatters.Binary;

namespace Arebis.Core.AspNet.Mvc.Localization
{
    /// <summary>
    /// Persists localization data to binary file for faster loading after application(pool) restart.
    /// Can be registered as Transient, Scoped or Singleton service for type <see cref="ILocalizationResourcePersistor"/>.
    /// </summary>
    public class BinaryFileLocalizationResourcePersistor : ILocalizationResourcePersistor
    {
        private readonly IOptions<LocalizationOptions> localizationOptions;
        private readonly ILogger<BinaryFileLocalizationResourcePersistor> logger;

        /// <summary>
        /// Constructs a BinaryFileLocalizationResourcePersistor.
        /// </summary>
        public BinaryFileLocalizationResourcePersistor(ILogger<BinaryFileLocalizationResourcePersistor> logger, IOptions<LocalizationOptions> localizationOptions)
        {
            this.localizationOptions = localizationOptions;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public event EventHandler? OnChanged;

        /// <inheritdoc/>
        public event EventHandler? OnSaved;

        /// <inheritdoc/>
        public LocalizationResourceSet? TryLoad()
        {
            // If cache file name defined, try loading from cache file:
            if (this.localizationOptions.Value.CacheFileName != null)
            {
                var filename = Environment.ExpandEnvironmentVariables(this.localizationOptions.Value.CacheFileName);
                try
                {
                    // Load resources from file:
                    using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                    {
#pragma warning disable SYSLIB0011
                        var resources = new BinaryFormatter().Deserialize(stream) as LocalizationResourceSet;
#pragma warning restore SYSLIB0011
                        logger.LogInformation("Localization resources loaded form cache file \"{filename}\".", filename);
                        return resources;
                    }
                }
                catch (FileNotFoundException)
                {
                    logger.LogInformation("Localization cache file \"{filename}\" not found.", filename);
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "Unexpected exception while reading localization cache file \"{filename}\".", filename);
                }
            }

            // In all other cases, return null:
            return null;
        }

        /// <inheritdoc/>
        public void Changed(EventArgs e)
        {
            this.OnChanged?.Invoke(this, e);
        }

        /// <inheritdoc/>
        public void Saved(EventArgs e)
        {
            this.OnSaved?.Invoke(this, e);
        }
    
        /// <inheritdoc/>
        public void TrySave(LocalizationResourceSet? resourceSet)
        {
            if (this.localizationOptions.Value.CacheFileName != null)
            {
                var filename = Environment.ExpandEnvironmentVariables(this.localizationOptions.Value.CacheFileName);
                if (resourceSet != null)
                {
                    try
                    {
                        // Ensure directory is present:
                        var file = new FileInfo(filename);
                        file.Directory?.Create(); // Create if missing

                        // Write cache file:
                        using (var stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
                        {
#pragma warning disable SYSLIB0011
                            new BinaryFormatter().Serialize(stream, resourceSet);
#pragma warning restore SYSLIB0011
                        }
                        logger.LogInformation("Localization resources saved to cache file \"{filename}\".", filename);
                    }
                    catch (Exception ex)
                    {
                        logger.LogWarning(ex, "Unexpected exception while writing localization cache file \"{filename}\".", filename);
                    }
                    finally
                    {
                        this.Saved(EventArgs.Empty);
                    }
                }
                else
                {
                    try
                    {
                        System.IO.File.Delete(filename);
                    }
                    catch (Exception ex)
                    {
                        logger.LogWarning(ex, "Unexpected exception while deleting localization cache file \"{filename}\".", filename);
                    }
                    finally
                    {
                        this.Saved(EventArgs.Empty);
                    }
                }
            }
        }
    }
}
