using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Arebis.Core.EntityFramework.ValueConversion
{
    /// <summary>
    /// A value converter that stores TimeSpans as hours.
    /// </summary>
    public class TimeSpanHoursConverter : ValueConverter<TimeSpan, Double>
    {
        /// <inheritdoc/>
        public TimeSpanHoursConverter()
            : base(
                  value => value.TotalHours,
                  value => TimeSpan.FromHours(value),
                  null)
        { }
    }
}
