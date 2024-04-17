using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Arebis.Core.EntityFramework.ValueConversion
{
    /// <summary>
    /// A value converter that stores TimeSpans as days.
    /// </summary>
    public class TimeSpanDaysValueConverter : ValueConverter<TimeSpan, Double>
    {
        /// <inheritdoc/>
        public TimeSpanDaysValueConverter()
            : base(
                  value => value.TotalDays,
                  value => TimeSpan.FromDays(value),
                  null)
        { }
    }
}
