using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.EntityFramework.ValueConversion
{
    /// <summary>
    /// A value comparer to compare TimeOnly values.
    /// </summary>
    public class TimeOnlyValueComparer : ValueComparer<TimeOnly>
    {
        /// <inheritdoc/>
        public TimeOnlyValueComparer() : base(
            (t1, t2) => t1.Ticks == t2.Ticks,
            t => t.GetHashCode())
        { }
    }
}
