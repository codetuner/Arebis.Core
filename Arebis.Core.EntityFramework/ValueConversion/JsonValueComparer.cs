using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Arebis.Core.EntityFramework.ValueConversion
{
    /// <summary>
    /// A value comparer that compares values by their Json representation.
    /// </summary>
    public class JsonValueComparer<T> : ValueComparer<T>
    {
        /// <summary>
        /// Options to use when serializing values to compare.
        /// </summary>
        public static JsonSerializerOptions? SerializerOptions { get; set; } = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            AllowTrailingCommas = true,
            IgnoreReadOnlyFields = true,
            IgnoreReadOnlyProperties = true,
        };

        /// <inheritdoc/>
        public JsonValueComparer()
            : base(
                  (o1, o2) => (o1 == null && o2 == null) || (o1 != null && o2 != null) && JsonSerializer.Serialize<T>(o1, SerializerOptions) == JsonSerializer.Serialize<T>(o2, SerializerOptions),
                  (o) => o == null ? 0 : JsonSerializer.Serialize<T>(o, SerializerOptions).GetHashCode())
        { }
    }
}
