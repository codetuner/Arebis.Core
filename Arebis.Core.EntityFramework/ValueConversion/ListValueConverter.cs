using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.Core.EntityFramework.ValueConversion
{
    /// <summary>
    /// Converts a list into a string representation.
    /// </summary>
    /// <typeparam name="TList"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    public class ListValueConverter<TList, TItem> : ValueConverter<TList, string?>
        where TList : ICollection<TItem>, new()
    {
        private static readonly IFormatProvider formatProvider = System.Globalization.CultureInfo.InvariantCulture;

        /// <inheritdoc/>
        public ListValueConverter()
            : this(";")
        { }

        /// <inheritdoc/>
        public ListValueConverter(string separator)
            : this(separator, String.Empty, String.Empty)
        { }

        /// <inheritdoc/>
        public ListValueConverter(string separator, string prefix, string postfix)
            : base(
                value => Serialize(value, separator, prefix, postfix),
                value => Deserialize(value!, separator, prefix, postfix),
                null)
        { }

        static string? Serialize(TList value, string separator, string prefix, string postfix)
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
                    sep = separator;
                    sb.Append(Convert.ChangeType(item, typeof(string), formatProvider));
                }
                sb.Append(postfix);
                return sb.ToString();
            }
        }

        static TList Deserialize(string value, string separator, string prefix, string postfix)
        {
            if (value == null || value.Length == 0)
            {
                return default!;
            }
            else
            {
                value = value.Substring(prefix.Length, value.Length - prefix.Length - postfix.Length);
                var result = new TList();
                foreach (var part in value.Split(separator))
                {
                    if (part.Length > 0)
                        result.Add((TItem)Convert.ChangeType(part, typeof(TItem), formatProvider));
                    else
                        result.Add(default!);
                }
                return result;
            }
        }
    }
}
