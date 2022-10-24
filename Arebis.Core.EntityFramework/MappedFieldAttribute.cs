using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.Core.EntityFramework
{
    /// <summary>
    /// Marks a field as being mapped.
    /// Allowd mapping database columns to private fields without exposing a public property.
    /// </summary>
    /// <see href="https://docs.microsoft.com/en-us/ef/core/modeling/backing-field?tabs=data-annotations#field-only-properties"/>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class MappedFieldAttribute : Attribute
    {
        /// <inheritdoc/>
        public MappedFieldAttribute()
        { }
    }
}
