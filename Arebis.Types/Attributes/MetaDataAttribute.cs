using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.Types.Attributes
{
    /// <summary>
    /// A MateDataAttribute allows adding metadata to code elements.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class MetaDataAttribute : Attribute
    {
        /// <summary>
        /// Adds metadata to a code element.
        /// </summary>
        public MetaDataAttribute(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Name of the metadata property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of the metadata property.
        /// </summary>
        public object Value { get; set; }
    }
}
