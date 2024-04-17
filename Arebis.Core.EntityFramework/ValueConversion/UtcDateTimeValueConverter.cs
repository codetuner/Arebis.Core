using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Arebis.Core.EntityFramework.ValueConversion
{
    /// <summary>
    /// A value converter that stores DateTimes in UTC.
    /// </summary>
    public class UtcDateTimeValueConverter : ValueConverter<DateTime, DateTime>
    {
        /// <inheritdoc/>
        public UtcDateTimeValueConverter()
            : base(
                  value => (value.Kind == DateTimeKind.Local) ? value.ToUniversalTime() : value,
                  value => (value.Kind == DateTimeKind.Unspecified) ? new DateTime(value.Ticks, DateTimeKind.Utc) : value,
                  null)
        { }
    }
}
