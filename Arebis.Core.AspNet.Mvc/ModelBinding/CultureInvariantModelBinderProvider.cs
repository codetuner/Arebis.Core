using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace Arebis.Core.AspNet.Mvc.ModelBinding
{
    /// <summary>
    /// Provides a model binder that binds values using the given culture instead of the ambient culture.
    /// </summary>
    public class CultureInvariantModelBinderProvider : IModelBinderProvider
    {
        private readonly CultureInfo culture;

        /// <summary>
        /// Constructs a CultureInvariantModelBinderProvider for the InvariantCulture.
        /// </summary>
        public CultureInvariantModelBinderProvider()
            : this(CultureInfo.InvariantCulture)
        { }

        /// <summary>
        /// Constructs a CultureInvariantModelBinderProvider for the given culture.
        /// </summary>
        public CultureInvariantModelBinderProvider(CultureInfo culture)
        {
            this.culture = culture;
        }

        /// <inheritdoc/>
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.UnderlyingOrModelType == typeof(Decimal))
            {
                return new CultureInvariantModelBinder(this.culture);
            }
            else
            {
                return null;
            }
        }
    }
}
