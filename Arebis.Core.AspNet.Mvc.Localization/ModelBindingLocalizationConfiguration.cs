using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.AspNet.Mvc.Localization
{
    /// <summary>
    /// Localizes default model binding error messages.
    /// To be registered as Singleton service for type <see cref="IConfigureOptions{MvcOptions}"/>.
    /// </summary>
    /// <seealso href="https://www.waretec.at/localizing-asp-net-core-5-apis-statically-typed/"/>
    /// <seealso href="https://learn.microsoft.com/en-us/archive/blogs/mvpawardprogram/aspnetcore-mvc-error-message"/>
    public class ModelBindingLocalizationConfiguration : IConfigureOptions<MvcOptions>
    {
        private readonly IStringLocalizer localizer;

        /// <summary>
        /// Constructs a ModelBindingLocalizationConfiguration.
        /// </summary>
        public ModelBindingLocalizationConfiguration(IStringLocalizer localizer)
        {
            this.localizer = localizer;
        }

        /// <summary>
        /// Configures the messages of the ModelBindingMessageProvider.
        /// </summary>
        public void Configure(MvcOptions options)
        {
            options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => localizer.GetString("The value '{0}' is invalid.", x));
            options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => localizer.GetString("The field {0} must be a number.", x));
            options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => localizer.GetString("A value for the '{0}' property was not provided.", x));
            options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => localizer.GetString("The value '{0}' is not valid for {1}.", x, y));
            options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => localizer.GetString("A value is required."));
            options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(x => localizer.GetString("The supplied value is invalid for {0}.", x));
            options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => localizer.GetString("The value '{0}' is invalid.", x));
            options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(x => localizer.GetString("The value '{0}' is not valid.", x));
            options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => localizer.GetString("The supplied value is invalid."));
            options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => localizer.GetString("The field must be a number."));
            options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => localizer.GetString("A non-empty request body is required."));
        }
    }
}
