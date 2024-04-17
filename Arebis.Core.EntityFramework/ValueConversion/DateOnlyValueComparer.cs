using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.EntityFramework.ValueConversion
{
    /// <summary>
    /// A value comparer to compare DateOnly values.
    /// </summary>
    public class DateOnlyValueComparer : ValueComparer<DateOnly>
    {
        /// <inheritdoc/>
        public DateOnlyValueComparer() : base(
            (d1, d2) => d1.DayNumber == d2.DayNumber,
            d => d.GetHashCode())
        { }
    }
}
