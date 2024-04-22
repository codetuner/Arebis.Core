using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Arebis.Core.AspNet.Mvc.ModelBinding
{
    /// <summary>
    /// The IdentifierEnumModelBinder binds int values to enums even if there is no matching enum value
    /// provided the enum type has the [IdentifierEnum] attribute.
    /// </summary>
    public class IdentifierEnumModelBinder : IModelBinder
    {
        /// <inheritdoc/>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // Context is required:
            if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            var value = valueProviderResult.FirstValue;
            if (string.IsNullOrEmpty(value))
            {
                if (bindingContext.ModelMetadata.IsRequired)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, "The value '' is invalid.");
                }
                return Task.CompletedTask;
            }

            var enumType = bindingContext.ModelMetadata.UnderlyingOrModelType;
            if (Enum.GetUnderlyingType(enumType) == typeof(Int32) && Int32.TryParse(value, out int int32Value))
            {
                // If numerical, bind without validating:
                bindingContext.Result = ModelBindingResult.Success(Enum.ToObject(enumType, int32Value));
            }
            else if (Enum.GetUnderlyingType(enumType) == typeof(Int64) && Int64.TryParse(value, out long int64Value))
            {
                // If numerical, bind without validating:
                bindingContext.Result = ModelBindingResult.Success(Enum.ToObject(enumType, int64Value));
            }
            else if (Enum.TryParse(enumType, value, out object? enumValue))
            {
                bindingContext.Result = ModelBindingResult.Success(enumValue);
            }
            else
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, $"The value '{value}' is invalid.");
            }

            return Task.CompletedTask;
        }
    }
}
