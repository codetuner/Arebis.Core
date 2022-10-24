using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.Core.EntityFramework
{
    /// <summary>
    /// Marks the mapped inheritance type discriminator property.
    /// Requires the DbContext to inherit from BaseDbContext.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class TypeDiscriminatorAttribute : Attribute
    {
        /// <inheritdoc/>
        public TypeDiscriminatorAttribute()
        { }
    }
}
