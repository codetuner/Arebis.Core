using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.Core.EntityFramework.ValueConversion
{
    /// <summary>
    /// Attribute to declare a specific value converter for the decorated property.
    /// Requires the DbContext to inherit from BaseDbContext.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ConverterAttribute : Attribute
    {
        /// <inheritdoc/>
        public ConverterAttribute(Type converterType, params object[] constructorArgs)
        {
            this.ConverterType = converterType;
            this.ConstructorArgs = constructorArgs;
        }

        /// <summary>
        /// Type of converter.
        /// </summary>
        public Type ConverterType { get; set; }

        /// <summary>
        /// Constructor arguments for the converter.
        /// </summary>
        public object[] ConstructorArgs { get; set; }
    }
}
