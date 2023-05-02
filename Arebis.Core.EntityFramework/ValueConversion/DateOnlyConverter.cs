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
    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        /// <inheritdoc/>
        public DateOnlyConverter()
            : base(
                value => value.ToDateTime(TimeOnly.MinValue),
                value => DateOnly.FromDateTime(value),
                null)
        { }
    }
}
