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
    /// Acts as IAsyncActionFilter for MVC and as IAsyncPageFilter for Razor Pages.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ModelStateLocalizationFilter : Attribute, IAsyncActionFilter, IAsyncPageFilter
    {
        #region Constructor using depency injection

        private readonly IStringLocalizer localizer;
        private readonly ILogger<ModelStateLocalizationFilter> logger;
        private readonly ModelStateLocalizationMapping mapping;

        /// <summary>
        /// Constructs a <see cref="ModelStateLocalizationFilter"/>.
        /// </summary>
        public ModelStateLocalizationFilter(ILogger<ModelStateLocalizationFilter> logger, ModelStateLocalizationMapping mapping, IStringLocalizer localizer)
        {
            this.logger = logger;
            this.mapping = mapping;
            this.localizer = localizer;            
        }

        #endregion

        #region IAsyncActionFilter implementation to support MVC

        async Task IAsyncActionFilter.OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next.Invoke();

            this.Localize(context.ModelState);
        }

        #endregion

        #region IAsyncPageFilter implementation to support Razor Pages
        
        Task IAsyncPageFilter.OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }

        async Task IAsyncPageFilter.OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            await next.Invoke();

            this.Localize(context.ModelState);
        }
        
        #endregion

        #region Internal implementation

        /// <summary>
        /// Internal implementation trying to localize ModelState errors using a ModelStateLocalizationMapping.
        /// </summary>
        private void Localize(ModelStateDictionary modelState)
        {
            if (mapping == null || mapping.Mapping == null || mapping.Mapping.Count == 0) return;

            // Run over all ModelState items:
            foreach (var item in modelState)
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

        #endregion
    }
}
