using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.EntityFramework
{
    /// <summary>
    /// Specifies the value of the type discriminator property that corresponds to the decorated type.
    /// The root type of the hierarchy must have a [TypeDiscriminator] attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TypeDiscriminatorValueAttribute : Attribute
    {
        /// <summary>
        /// Defines a value for the type discriminator property to match this entity type.
        /// </summary>
        public TypeDiscriminatorValueAttribute(object value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Value of the discriminator property.
        /// </summary>
        public object Value { get; set; }
    }
}
