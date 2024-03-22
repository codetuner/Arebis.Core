using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.EntityFramework
{
    /// <summary>
    /// BaseDbContext extension helpers.
    /// </summary>
    public static class BaseDbContextExtensions
    {
        /// <summary>
        /// Installs an interceptor that validates entities before saving.
        /// </summary>
        /// <param name="optionsBuilder">OnConfiguring method parameter</param>
        /// <param name="validatePropertyValues">
        /// Whether to validate not only whether the property has a value (if [Required]),
        /// but also whether the value is valid towards other annotations as [Range] or [StringLength].
        /// True by default.
        /// </param>
        /// <param name="alsoValidateUnchanged">
        /// Whether to also validate unchanged entities. This can be required to validate rules based
        /// on the count of related entities. By default false: only modified and added entities are validated.
        /// </param>
        public static DbContextOptionsBuilder UseValidation(this DbContextOptionsBuilder optionsBuilder, bool validatePropertyValues = true, bool alsoValidateUnchanged = false)
        {
            optionsBuilder.AddInterceptors(new ValidationInterceptor(validatePropertyValues, alsoValidateUnchanged));
            return optionsBuilder;
        }

        /// <summary>
        /// Installs an interceptor to support [StoreEmptyAsNull] attributes.
        /// </summary>
        /// <param name="optionsBuilder">OnConfiguring method parameter</param>
        public static DbContextOptionsBuilder UseStoreEmptyAsNullAttributes(this DbContextOptionsBuilder optionsBuilder)
        {
            // StoreEmptyAsNullInterceptor is a IMaterializationInterceptor which is a type of ISingletonInterceptor
            // and should be reused, see: https://github.com/dotnet/efcore/issues/29330
            cachedStoreEmptyAsNullInterceptor ??= new StoreEmptyAsNullInterceptor();
            optionsBuilder.AddInterceptors(cachedStoreEmptyAsNullInterceptor);
            return optionsBuilder;
        }

        private static StoreEmptyAsNullInterceptor? cachedStoreEmptyAsNullInterceptor = null;

        /// <summary>
        /// Installs an interceptor to trim changed strings before storing them.
        /// </summary>
        /// <param name="optionsBuilder">OnConfiguring method parameter</param>
        /// <param name="storeEmptyAsNull">Whether to store empty strings as null if the property is nullable.</param>
        public static DbContextOptionsBuilder UseStringTrimming(this DbContextOptionsBuilder optionsBuilder, bool storeEmptyAsNull = true)
        {
            optionsBuilder.AddInterceptors(new StringTrimmingInterceptor(storeEmptyAsNull));
            return optionsBuilder;
        }
    }
}
