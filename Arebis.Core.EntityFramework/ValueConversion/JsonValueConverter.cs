using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arebis.Core.EntityFramework.ValueConversion
{
    /// <summary>
    /// Non-generic JsonConverter class holding serializer options.
    /// </summary>
    public abstract class JsonValueConverter
    {
        /// <summary>
        /// Options to use when (de)serializing values.
        /// </summary>
        public static JsonSerializerOptions? SerializerOptions { get; set; } = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            AllowTrailingCommas = true,
            IgnoreReadOnlyFields = true,
            IgnoreReadOnlyProperties = true,            
        };
    }

    /// <summary>
    /// A value converter that stores objects as Json.
    /// </summary>
    public class JsonValueConverter<T> : ValueConverter<T, string?>
    {
        /// <inheritdoc/>
        public JsonValueConverter()
            : base(
                  value => JsonSerializer.Serialize(value, JsonValueConverter.SerializerOptions),
                  value => JsonSerializer.Deserialize<T>(value ?? "null", JsonValueConverter.SerializerOptions)!,
                  null)
        { }
    }
}
