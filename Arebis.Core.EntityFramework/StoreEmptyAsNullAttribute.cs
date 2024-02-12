using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.EntityFramework
{
    /// <summary>
    /// Specifies that the decorated property is to be stored as a NULL value
    /// when it is empty (or whitespace string). When materializing, NULL values
    /// are converted to empty collections or empty strings.
    /// Requires the DbContext to inherit from BaseDbContext and UseStoreEmptyAsNullAttribute()
    /// to be invoked from the context's OnConfiguring method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class StoreEmptyAsNullAttribute : Attribute
    {
        /// <summary>
        /// The instance type to create when a NULL value is dematerialized.
        /// </summary>
        public Type? InstanceType { get; init; }

        /// <summary>
        /// Whether to allow Null to be stored. By default true. If false,
        /// this means that empty strings or collections are not allowed.
        /// </summary>
        public bool AllowNullOnStorage { get; set; } = true;
    }
}
