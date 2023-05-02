using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.EntityFramework.ValueConversion
{
    /// <summary>
    /// A value converter that stores DateOnly as DateTime.
    /// </summary>
    public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
    {
        /// <inheritdoc/>
        public TimeOnlyConverter()
            : base(
                value => value.ToTimeSpan(),
                value => TimeOnly.FromTimeSpan(value),
                null)
        { }
    }
}
