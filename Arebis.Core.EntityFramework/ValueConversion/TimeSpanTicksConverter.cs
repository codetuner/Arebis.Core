using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Arebis.Core.EntityFramework.ValueConversion
{
    /// <summary>
    /// A value converter that stores TimeSpans as ticks.
    /// </summary>
    public class TimeSpanTicksConverter : ValueConverter<TimeSpan, long>
    {
        /// <inheritdoc/>
        public TimeSpanTicksConverter()
            : base(
                  value => value.Ticks,
                  value => TimeSpan.FromTicks(value),
                  null)
        { }
    }
}
