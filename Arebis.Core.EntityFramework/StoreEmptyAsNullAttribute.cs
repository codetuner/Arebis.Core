using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.EntityFramework
{
    /// <summary>
    /// Specifies that the decorated string property is to be stored as a NULL value
    /// when it is an empty string or whitespace string. When materializing, NULL values
    /// are converted to empty strings.
    /// Requires the DbContext to inherit from BaseDbContext and UseStoreEmptyAsNullAttribute()
    /// to be invoked from the context's OnConfiguring method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class StoreEmptyAsNullAttribute : Attribute
    { }
}
