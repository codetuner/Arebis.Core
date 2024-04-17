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
    public class TimeSpanHoursValueConverter : ValueConverter<TimeSpan, Double>
    {
        /// <inheritdoc/>
        public TimeSpanHoursValueConverter()
            : base(
                  value => value.TotalHours,
                  value => TimeSpan.FromHours(value),
                  null)
        { }
    }
}
