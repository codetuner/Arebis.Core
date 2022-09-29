using Arebis.Core.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Arebis.Core.AspNet.Mvc.Localization
{
    /// <summary>
    /// Persists localization data to Json file for faster loading after application(pool) restart.
    /// Can be registered as Transient, Scoped or Singleton service for type <see cref="ILocalizationResourcePersistor"/>.
    /// </summary>
    public class JsonFileLocalizationResourcePersistor : ILocalizationResourcePersistor
    {
        private readonly IOptions<LocalizationOptions> localizationOptions;
        private readonly ILogger<JsonFileLocalizationResourcePersistor> logger;

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
        public LocalizationResourceSet? TryLoad()
        {
            // If cache file name defined, try loading from cache file:
            if (this.localizationOptions.Value.CacheFileName != null)
            {
                var filename = Environment.ExpandEnvironmentVariables(this.localizationOptions.Value.CacheFileName);
                try
                {
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
    }
}
