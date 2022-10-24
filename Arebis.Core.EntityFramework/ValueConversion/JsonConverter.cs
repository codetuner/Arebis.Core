using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;

namespace Arebis.Core.EntityFramework.ValueConversion
{
    /// <summary>
    /// A value converter that stores objects as Json.
    /// </summary>
    public class JsonConverter<T> : ValueConverter<T, string?>
    {
        /// <inheritdoc/>
        public JsonConverter()
            : base(
                  value => JsonSerializer.Serialize(value, (JsonSerializerOptions?)null),
                  value => JsonSerializer.Deserialize<T>(value ?? "null", (JsonSerializerOptions?)null)!,
                  null)
        { }
    }
}
