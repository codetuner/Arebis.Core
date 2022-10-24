using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.Core.EntityFramework.ValueConversion
{
    /// <summary>
    /// Converts a Dictionary to a string representation.
    /// </summary>
    public class DictionaryConverter<TDictionary, TKey, TValue> : ValueConverter<TDictionary, string?>
        where TDictionary : ICollection<KeyValuePair<TKey, TValue>>, new()
    {
        private static readonly IFormatProvider formatProvider = System.Globalization.CultureInfo.InvariantCulture;

        /// <inheritdoc/>
        public DictionaryConverter()
            : this(";", "=")
        { }

        /// <inheritdoc/>
        public DictionaryConverter(string pairseparator, string valueseparator)
            : this(pairseparator, valueseparator, String.Empty, String.Empty)
        { }

        /// <inheritdoc/>
        public DictionaryConverter(string pairseparator, string valueseparator, string prefix, string postfix)
            : base(
                value => Serialize(value, pairseparator, valueseparator, prefix, postfix),
                value => Deserialize(value, pairseparator, valueseparator, prefix, postfix),
                null)
        { }

        static string? Serialize(TDictionary value, string pairseparator, string valueseparator, string prefix, string postfix)
        {
            if (value == null || value.Count == 0)
            {
                return null;
            }
            else
            {
                var sb = new StringBuilder();
                sb.Append(prefix);
                var sep = String.Empty;
                foreach (var item in value)
                {
                    sb.Append(sep);
                    sep = pairseparator;
                    sb.Append(Convert.ChangeType(item.Key, typeof(string), formatProvider));
                    sb.Append(valueseparator);
                    sb.Append(Convert.ChangeType(item.Value, typeof(string), formatProvider));
                }
                sb.Append(postfix);
                return sb.ToString();
            }
        }

        static TDictionary Deserialize(string? value, string pairseparator, string valueseparator, string prefix, string postfix)
        {
            if (value == null || value.Length == 0)
            {
                return default!;
            }
            else
            {
                value = value.Substring(prefix.Length, value.Length - prefix.Length - postfix.Length);
                var result = new TDictionary();
                foreach (var part in value.Split(pairseparator))
                {
                    if (part.Length > 0)
                    {
                        var pair = part.Split(valueseparator, 2);
                        result.Add(new KeyValuePair<TKey, TValue>((TKey)Convert.ChangeType(pair[0], typeof(TKey), formatProvider), (TValue)Convert.ChangeType(pair[1], typeof(TValue), formatProvider)));
                    }
                }
                return result;
            }
        }
    }
}
