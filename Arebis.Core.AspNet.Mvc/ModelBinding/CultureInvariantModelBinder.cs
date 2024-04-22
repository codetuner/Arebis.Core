using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace Arebis.Core.AspNet.Mvc.ModelBinding
{
    /// <summary>
    /// A model binder that binds values using the given culture instead of the ambient culture.
    /// </summary>
    public class CultureInvariantModelBinder : IModelBinder
    {
        private readonly CultureInfo culture;

        /// <summary>
        /// Constructs a CultureInvariantModelBinder using the given culture.
        /// </summary>
        public CultureInvariantModelBinder(CultureInfo culture)
        {
            this.culture = culture;
        }

        /// <inheritdoc/>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None) return Task.CompletedTask;
            
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;
            if (value == null)
            {
                bindingContext.Result = ModelBindingResult.Success(null);
            }
            else if (bindingContext.ModelMetadata.UnderlyingOrModelType == typeof(decimal))
            {
                if (Decimal.TryParse(value, NumberStyles.Any, culture, out var decimalValue))
                {
                    bindingContext.Result = ModelBindingResult.Success(decimalValue);
                }
                else
                {
                    bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, $"The value '{value}' is invalid.");
                }
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return Task.CompletedTask;
        }
    }
}
