using Arebis.Core.Localization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Arebis.Core.AspNet.Mvc.Localization.ModelStateLocalizationMapping;

namespace Arebis.Core.AspNet.Mvc.Localization
{
    /// <summary>
    /// Localizes error messages from ModelState.
    /// </summary>
    public class ModelStateLocalizationFilter : ActionFilterAttribute
    {
        private readonly IStringLocalizer localizer;
        private readonly ILogger<ModelStateLocalizationFilter> logger;
        private readonly ModelStateLocalizationMapping mapping;

        /// <summary>
        /// Constructs a ModelStateLocalizationFilter.
        /// </summary>
        public ModelStateLocalizationFilter(ILogger<ModelStateLocalizationFilter> logger, ModelStateLocalizationMapping mapping, IStringLocalizer localizer)
        {
            this.logger = logger;
            this.mapping = mapping;
            this.localizer = localizer;            
        }

        /// <summary>
        /// Tries to localize ModelState errors using a ModelStateLocalizationMapping.
        /// </summary>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (mapping == null || mapping.Mapping == null || mapping.Mapping.Count == 0) return;

            // Run over all ModelState items:
            foreach (var item in context.ModelState)
            {
                // Rebuild a new Errors list with (localized and non-localized) errors:
                var itemValueErrors = item.Value.Errors.ToList();
                item.Value.Errors.Clear();
                foreach (var err in itemValueErrors)
                {
                    // Search for a mapping pattern that matches the error message;
                    // if one is found, add a new error with localized message:
                    var modelErrorHasBeenLocalized = false;
                    foreach (var map in mapping.Mapping)
                    {
                        var match = map.Pattern.Match(err.ErrorMessage);
                        if (match.Success)
                        {
                            if (map.ArgLocalization != null && map.ArgLocalization.Length > 0)
                            {
                                // Build args array and localize the argument too if its corresponding ArgLocalization value is true:
                                var args = new object[map.ArgLocalization.Length];
                                for (int i = 0; i < args.Length; i++)
                                {
                                    var value = match.Groups[i + 1].Value;
                                    if (map.ArgLocalization[i])
                                    {
                                        value = localizer.GetString(value);
                                    }
                                    args[i] = value;
                                }
                                // Add a new error with localized error message (with args):
                                item.Value.Errors.Add(localizer.GetString(map.LocalizationKey, args));
                            }
                            else
                            {
                                // Add a new error with localized error message (without args):
                                item.Value.Errors.Add(localizer.GetString(map.LocalizationKey));
                            }
                            // Localization done; skip further processing:
                            modelErrorHasBeenLocalized = true;
                            break;
                        }
                    }
                    // If error was not to localize, add original error back:
                    if (!modelErrorHasBeenLocalized)
                    {
                        item.Value.Errors.Add(err);
                        logger.LogWarning("No ModelStateLocalization pattern matched error \"{errorMessage}\" for key \"{key}\".", err.ErrorMessage, item.Key);
                    }
                }
            }
        }
    }
}
