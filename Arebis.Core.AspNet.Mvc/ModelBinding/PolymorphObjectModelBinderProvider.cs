using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System.Text.Json.Serialization;

namespace Arebis.Core.AspNet.Mvc.ModelBinding
{
    // Based on the 'Polymorphic model binding' in
    // https://learn.microsoft.com/en-us/aspnet/core/mvc/advanced/custom-model-binding?view=aspnetcore-8.0#custom-model-binder-sample

    /// <summary>
    /// Provides a model binder for complex plymorphic types.
    /// Identifies the subtypes by means of a "~.$type" value that matches the descriminator on a [JsonDerivedType] attribute on the base type.
    /// </summary>
    public class PolymorphObjectModelBinderProvider : IModelBinderProvider
    {
        private bool Reentered = false;

        /// <inheritdoc />
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.IsComplexType && !context.Metadata.IsCollectionType && !Reentered)
            {
                Reentered = true;
                var binders = new Dictionary<string, (ModelMetadata, IModelBinder)>();
                var typeAttributes = (context.Metadata as DefaultModelMetadata)?.Attributes?.TypeAttributes;
                if (typeAttributes != null)
                {
                    foreach (var attr in typeAttributes.OfType<JsonDerivedTypeAttribute>())
                    {
                        var modelMetadata = context.MetadataProvider.GetMetadataForType(attr.DerivedType);
                        binders[attr.TypeDiscriminator?.ToString() ?? String.Empty] = (modelMetadata, context.CreateBinder(modelMetadata));
                    }
                }
                Reentered = false;

                if (binders.Any())
                {
                    return new PolymorphObjectModelBinder(binders);
                }
            }

            // In all other cases, this provider does not provide a binder:
            return null;
        }
    }
}
