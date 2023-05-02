using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.Core.EntityFramework.ValueConversion
{
    /// <summary>
    /// Attribute to declare a specific value converter (and comparer) for the decorated property.
    /// Requires the DbContext to inherit from BaseDbContext.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ConverterAttribute : Attribute
    {
        /// <inheritdoc/>
        public ConverterAttribute(Type converterType, params object[] constructorArgs)
        {
            this.ConverterType = converterType;
            this.ConverterConstructorArgs = constructorArgs;
        }

        /// <inheritdoc/>
        public ConverterAttribute(Type converterType, Type comparerType)
            : this(converterType, null, comparerType, null)
        { }

        /// <inheritdoc/>
        public ConverterAttribute(Type converterType, object[]? converterConstructorArgs, Type comparerType, object[]? comparerConstructorArgs)
        {
            this.ConverterType = converterType;
            this.ConverterConstructorArgs = converterConstructorArgs;
            this.ComparerType = comparerType;
            this.ComparerConstructorArgs = comparerConstructorArgs;
        }

        /// <summary>
        /// Type of converter.
        /// </summary>
        public Type ConverterType { get; set; }

        /// <summary>
        /// Constructor arguments for the converter.
        /// </summary>
        public object[]? ConverterConstructorArgs { get; set; }

        /// <summary>
        /// Type of comparer.
        /// </summary>
        public Type? ComparerType { get; set; }

        /// <summary>
        /// Constructor arguments for the comparer.
        /// </summary>
        public object[]? ComparerConstructorArgs { get; set; }
    }
}
