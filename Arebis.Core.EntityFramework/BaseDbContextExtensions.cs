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
        public static DbContextOptionsBuilder UseValidation(this DbContextOptionsBuilder optionsBuilder, bool validatePropertyValues = true)
        {
            optionsBuilder.AddInterceptors(new ValidationInterceptor(validatePropertyValues));
            return optionsBuilder;
        }

        /// <summary>
        /// Installs an interceptor to support [StoreEmptyAsNull] attributes.
        /// </summary>
        /// <param name="optionsBuilder">OnConfiguring method parameter</param>
        public static DbContextOptionsBuilder UseStoreEmptyAsNullAttributes(this DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new StoreEmptyAsNullInterceptor());
            return optionsBuilder;
        }

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
