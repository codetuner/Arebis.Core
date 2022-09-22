using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Arebis.Core.AspNet.Mvc.Localization
{
    /// <summary>
    /// Provides access to the data model state localization mappings defined in ModelStateLocalization.json.
    /// To be registered as Singleton service for type <see cref="ModelStateLocalizationMapping"/>.
    /// </summary>
    public class ModelStateLocalizationMapping
    {
        private readonly ILogger<ModelStateLocalizationMapping> logger;
        
        /// <summary>
        /// Constructs a ModelStateLocalizationMapping.
        /// </summary>
        /// <param name="logger"></param>
        public ModelStateLocalizationMapping(ILogger<ModelStateLocalizationMapping> logger)
        {
            this.logger = logger;

            LoadMappings();
        }

        /// <summary>
        /// The loaded ModelStateLocalization mapping.
        /// </summary>
        public List<Map> Mapping { get; } = new();

        private void LoadMappings()
        {
            try
            {
                using (var stream = new FileStream("ModelStateLocalization.json", FileMode.Open, FileAccess.Read))
                {
                    var records = JsonSerializer.Deserialize<Record[]>(stream);
                    logger.LogInformation("DataAnnotationLocalization loaded form file.");

                    if (records != null)
                    {
                        this.Mapping.Clear();
                        foreach (var record in records)
                        {
                            this.Mapping.Add(new Map(record));
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                logger.LogInformation("ModelStateLocalization.json file not found. No ModelStateLocalization will be applied.");
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Unexpected exception while reading ModelStateLocalization.json file.");
            }
        }

        /// <summary>
        /// A record in the ModelStateLocalization.json file.
        /// </summary>
        public class Record
        {
            /// <summary>
            /// Pattern of the value to localize.
            /// </summary>
            [Required]
            public string Pattern { get; set; } = null!;

            /// <summary>
            /// Localization key to use to localize.
            /// </summary>
            public string LocalizationKey { get; set; } = null!;

            /// <summary>
            /// For each positional argument, whether or not it should be localized.
            /// </summary>
            public bool[]? ArgLocalization { get; set; }
        }

        /// <summary>
        /// A Model State Localization record.
        /// </summary>
        public class Map
        {
            /// <summary>
            /// Constructs a Model State Localization record.
            /// </summary>
            public Map(Record record)
                : this(record.Pattern, record.LocalizationKey, record.ArgLocalization)
            { }

            /// <summary>
            /// Constructs a Model State Localization record.
            /// </summary>
            public Map(string pattern, string localizationKey, bool[]? argLocalization)
                : this(new Regex(pattern, RegexOptions.Compiled), localizationKey, argLocalization)
            { }

            /// <summary>
            /// Constructs a Model State Localization record.
            /// </summary>
            public Map(Regex pattern, string localizationKey, bool[]? argLocalization)
            {
                Pattern = pattern;
                LocalizationKey = localizationKey;
                ArgLocalization = argLocalization;
            }

            /// <summary>
            /// Pattern of the value to localize.
            /// </summary>
            public Regex Pattern { get; init; }

            /// <summary>
            /// Localization key to use to localize.
            /// </summary>
            public string LocalizationKey { get; init; }

            /// <summary>
            /// For each positional argument, whether or not it should be localized.
            /// </summary>
            public bool[]? ArgLocalization { get; init; }
        }
    }
}
