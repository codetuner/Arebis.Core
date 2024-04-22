using Microsoft.AspNetCore.Mvc.ModelBinding;
using Arebis.Types.Attributes;

namespace Arebis.Core.AspNet.Mvc.ModelBinding
{
    /// <summary>
    /// Provides a model binder that binds int values to enums even if there is no matching enum value
    /// provided the enum type has the [IdentifierEnum] attribute.
    /// </summary>
    public class IdentifierEnumModelBinderProvider : IModelBinderProvider
    {
        /// <inheritdoc/>
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            // Context is required:
            if (context == null) throw new ArgumentNullException(nameof(context));

            // Only return an instance of this if the modeltype is an enum and has the [IdentifierEnum] attribute:
            if (context.Metadata.UnderlyingOrModelType.IsEnum)
            {
                if (context.Metadata.UnderlyingOrModelType.CustomAttributes.Any(ca => ca.AttributeType == typeof(IdentifierEnumAttribute)))
                {
                    return new IdentifierEnumModelBinder();
                }
            }

            // In all other cases, this provider does not provide a binder:
            return null;
        }
    }
}
