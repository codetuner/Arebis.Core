using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics.CodeAnalysis;

namespace Arebis.Core.Extensions
{
	/// <summary>
	/// Provides extension methods to the Object class.
	/// </summary>
	/// <remarks>
	/// As Extension Methods will be supported by the C# language, these
	/// methods will be changed into real Extension Methods.
	/// </remarks>
	public static class ObjectExtensions
	{
        /// <summary>
        /// Returns a dictionary with all properties of the object and their values.
        /// Returns null if given object is null.
        /// </summary>
        /// <param name="obj">The object to translate into a dictionary.</param>
        /// <param name="includeNullValues">Whether to include properties where the value is null in the returned dictionary.</param>
        [return: NotNullIfNotNull("obj")]
        public static IDictionary<string, object?>? ToDictionary(this object? obj, bool includeNullValues = false)
        {
            if (obj == null) return null;

            var result = new Dictionary<string, object?>();
            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {
                var value = property.GetValue(obj);
                if (value != null || includeNullValues)
                {
                    result[property.Name] = value;
                }
            }

            return result;
        }

        /// <summary>
        /// Tries to apply the selector on the source and returns it's value. On failure (i.e. a null reference in the selector path)
        /// return the onFailureResult.
        /// </summary>
        /// <typeparam name="TSource">Type of source object.</typeparam>
        /// <typeparam name="TResult">Type of result value.</typeparam>
        /// <param name="source">The source object.</param>
        /// <param name="selector">The selector to apply on the source to get the result.</param>
        /// <param name="onFailureResult">Alternative result to return on any failure.</param>
        /// <returns>The result of evaluating the selector on the source, or onFailureResult in case of an exception.</returns>
        public static TResult Try<TSource, TResult>(this TSource source, Func<TSource, TResult> selector, TResult onFailureResult)
        {
            try
            {
                return selector.Invoke(source);
            }
            catch
            {
                return onFailureResult;
            }
        }
    }
}
