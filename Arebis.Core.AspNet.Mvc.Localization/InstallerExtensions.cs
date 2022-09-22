using Arebis.Core.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.Core.AspNet.Mvc.Localization
{
    /// <summary>
    /// Localization installer methods.
    /// </summary>
    public static class InstallerExtensions
    {
        /// <summary>
        /// Installs base Localization From Source services with configuration.
        /// </summary>
        public static IServiceCollection AddLocalizationFromSource(this IServiceCollection services, IConfiguration configuration, Action<LocalizationOptions> optionsAction)
        {
            // Configure LocalizationOptions:
            services.Configure<LocalizationOptions>(options => {
                configuration.GetSection("Localization").Bind(options);
                optionsAction?.Invoke(options);
            });

            // Chain to overload:
            return services.AddLocalizationFromSource();
        }

        /// <summary>
        /// Installs base Localization From Source services.
        /// </summary>
        internal static IServiceCollection AddLocalizationFromSource(this IServiceCollection services)
        {
            // Setting up services:
            services.AddTransient<ILocalizationResourcePersistor, JsonFileLocalizationResourcePersistor>();
            services.TryAddSingleton<ILocalizationResourceProvider, LocalizationResourceProvider>();
            services.TryAddSingleton<IStringLocalizerFactory, StringLocalizerFactory>();
            services.TryAddSingleton<IStringLocalizer, Localizer>();
            services.TryAddSingleton(typeof(IStringLocalizer<>), typeof(Localizer<>));
            services.TryAddSingleton<IHtmlLocalizer, Localizer>();
            services.TryAddSingleton(typeof(IHtmlLocalizer<>), typeof(Localizer<>));
            services.TryAddSingleton<IViewLocalizer, Localizer>();

            // Return services for fluent syntax support:
            return services;
        }

        /// <summary>
        /// Adds Model Binding Localization From Source. Localizes binding errors and sets-up the
        /// DataAnnotationLocalizationActionFilter to localize data annotations without resource keys or strings.
        /// </summary>
        /// <remarks>Must execute before .AddMvc() .AddControllersWithViews().</remarks>
        public static IServiceCollection AddModelBindingLocalizationFromSource(this IServiceCollection services)
        {
            // To localize model binding messages as those created when an alfa-value is entered for a numeric field:
            // (Must execute before the program's services.AddMvc() or services.AddControllersWithViews(),
            //  or must be configured during those calls, see https://stackoverflow.com/a/41669552/323122)
            services.TryAddSingleton<IConfigureOptions<MvcOptions>, ModelBindingLocalizationConfiguration>();

            // Add ActionFilter to localize DataAnnotation attributes that do not have a resource key, as:
            // [Required]
            // (ActionFilter must be installed on controllers)
            services.TryAddSingleton<ModelStateLocalizationMapping>();

            // Return services for fluent syntax support:
            return services;
        }

        /// <summary>
        /// Adds data annotations localization from source for data annotations without explicit localizable error message.
        /// </summary>
        public static IMvcBuilder AddDataAnnotationsLocalizationFromSource(this IMvcBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            // To localize DataAnnotation attributes having a resource key as:
            // [Display(Name = "Age")]
            // [Required(ErrorMessage = "{0} is required.")]
            // (Uses IStringLocalizerFactory)
            builder.AddDataAnnotationsLocalization(options => {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                    factory.Create(type);
            });

            // Return builder for fluent syntax support:
            return builder;
        }

        /// <summary>
        /// Applies Localization from source.
        /// Currently an empty method, yet call it to limit compatibility issues with future releases.
        /// </summary>
        public static IApplicationBuilder UseLocalizationFromSource(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
