using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Arebis.Core.AspNet.Mvc.ModelBinding
{
    // Based on the 'Polymorphic model binding' in
    // https://learn.microsoft.com/en-us/aspnet/core/mvc/advanced/custom-model-binding?view=aspnetcore-8.0#custom-model-binder-sample

    /// <summary>
    /// A model binder for complex plymorphic types.
    /// Identifies the subtypes by means of a "~.$type" value that matches the descriminator on a [JsonDerivedType] attribute on the base type.
    /// </summary>
    public class PolymorphObjectModelBinder : IModelBinder
    {-
        private Dictionary<string, (ModelMetadata, IModelBinder)> binders;

        /// <summary>
        /// Constructs a PolymorphObjectModelBinder given binders for each of the concrete types.
        /// </summary>
        public PolymorphObjectModelBinder(Dictionary<string, (ModelMetadata, IModelBinder)> binders)
        {
            this.binders = binders;
        }

        /// <inheritdoc/>
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            // Retrieve $type discriminator:
            var typeNameProperty = ModelNames.CreatePropertyModelName(bindingContext.ModelName, "$type");
            var typeNamePropertyValue = bindingContext.ValueProvider.GetValue(typeNameProperty).FirstValue ?? String.Empty;

            IModelBinder modelBinder;
            ModelMetadata modelMetadata;
            if (binders.ContainsKey(typeNamePropertyValue))
            {
                // If $type discriminator given, use it's binder:
                (modelMetadata, modelBinder) = binders[typeNamePropertyValue];
            }
            else
            {
                // If no $type discriminator given, try binding the ModelType:
                var binder = binders.FirstOrDefault(b => b.Value.Item1.ModelType == bindingContext.ModelType);
                if (binder.Key != null)
                {
                    modelMetadata = binder.Value.Item1;
                    modelBinder = binder.Value.Item2;
                }
                else
                {
                    // If none found, Fail to support collection binding to probe for next elements:
                    bindingContext.Result = ModelBindingResult.Failed();
                    return;
                }
            }

            // Create a binding context:
            var newBindingContext = DefaultModelBindingContext.CreateBindingContext(
                bindingContext.ActionContext,
                bindingContext.ValueProvider,
                modelMetadata,
                bindingInfo: null,
                bindingContext.ModelName);

            // Perform binding:
            await modelBinder.BindModelAsync(newBindingContext);
            bindingContext.Result = newBindingContext.Result;

            if (newBindingContext.Result.IsModelSet)
            {
                // Setting the ValidationState ensures properties on derived types are correctly...
                bindingContext.ValidationState[newBindingContext.Result.Model!] = new ValidationStateEntry
                {
                    Metadata = modelMetadata,
                };
            }
        }
    }
}
