using Arebis.Core.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Arebis.Core.AspNet.Mvc.Localization
{
    /// <summary>
    /// Persists localization data to Json file for faster loading after application(pool) restart.
    /// Can be registered as Transient, Scoped or Singleton service for type <see cref="ILocalizationResourcePersistor"/>.
    /// </summary>
    public class JsonFileLocalizationResourcePersistor : ILocalizationResourcePersistor, IDisposable
    {
        private readonly IOptions<LocalizationOptions> localizationOptions;
        private readonly ILogger<JsonFileLocalizationResourcePersistor> logger;
        private FileSystemWatcher? fileSystemWatcher = null;

        /// <summary>
        /// Constructs a JsonFileLocalizationResourcePersistor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="localizationOptions"></param>
        public JsonFileLocalizationResourcePersistor(ILogger<JsonFileLocalizationResourcePersistor> logger, IOptions<LocalizationOptions> localizationOptions)
        {
            this.localizationOptions = localizationOptions;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public event EventHandler? OnChange;

        /// <inheritdoc/>
        public LocalizationResourceSet? TryLoad()
        {
            // If cache file name defined, try loading from cache file:
            if (this.localizationOptions.Value.CacheFileName != null)
            {
                var filename = Environment.ExpandEnvironmentVariables(this.localizationOptions.Value.CacheFileName);
                try
                {
                    // On first attempt, install a FileSystemWatcher to watch changes:
                    if (this.localizationOptions.Value.CacheFileWatched && this.fileSystemWatcher == null)
                    {
                        var file = new FileInfo(filename);
                        this.fileSystemWatcher = new FileSystemWatcher(file.Directory!.FullName, file.Name);
                        this.fileSystemWatcher.IncludeSubdirectories = false;
                        this.fileSystemWatcher.EnableRaisingEvents = true;
                        this.fileSystemWatcher.Changed += FileSystemWatcher_Changed;
                        this.fileSystemWatcher.Created += FileSystemWatcher_Created;
                    }

                    // Load resources from file:
                    using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                    {
                        var options = new JsonSerializerOptions()
                        {
                            AllowTrailingCommas = true,
                            ReadCommentHandling = JsonCommentHandling.Skip
                        };
                        var resources = JsonSerializer.Deserialize<LocalizationResourceSet>(stream, options);
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

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            this.OnChange?.Invoke(this, e);
        }

        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            this.OnChange?.Invoke(this, e);
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
                            JsonSerializer.Serialize(stream, resourceSet);
                        }
                        logger.LogInformation("Localization resources saved to cache file \"{filename}\".", filename);
                    }
                    catch (Exception ex)
                    {
                        logger.LogWarning(ex, "Unexpected exception while writing localization cache file \"{filename}\".", filename);
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
                }
            }
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            if (this.fileSystemWatcher != null)
            {
                this.fileSystemWatcher.Dispose();
                this.fileSystemWatcher = null;
            }
        }
    }
}
