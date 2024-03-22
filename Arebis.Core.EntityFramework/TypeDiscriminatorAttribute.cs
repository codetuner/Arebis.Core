using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.Core.EntityFramework
{
    /// <summary>
    /// Marks the hierarchy root type as having a type discriminator property.
    /// Concrete subtypes must have a [TypeDiscriminatorValue] attribute.
    /// If this type is concrete, it must als have the [TypeDiscriminatorValue] attribute.
    /// Requires the DbContext to inherit from BaseDbContext.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TypeDiscriminatorAttribute : Attribute
    {
        /// <summary>
        /// Defines a TypeDiscriminator property of type string.
        /// </summary>
        /// <param name="propertyName">The name of the discriminator property.</param>
        public TypeDiscriminatorAttribute(string propertyName)
            : this(propertyName, typeof(string))
        { }

        /// <summary>
        /// Defines a TypeDiscriminator property.
        /// </summary>
        /// <param name="propertyName">The name of the discriminator property.</param>
        /// <param name="propertyType">The type of the discriminator property.</param>
        public TypeDiscriminatorAttribute(string propertyName, Type propertyType)
        {
            PropertyName = propertyName;
            PropertyType = propertyType;
        }
        
        /// <summary>
        /// Name of the discriminator property.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Type of the discriminator property.
        /// </summary>
        public Type PropertyType { get; set; }
    }
}
