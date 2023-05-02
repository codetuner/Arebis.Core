﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.EntityFramework.ValueConversion
{
    /// <summary>
    /// A value comparer to compare TimeSpan values.
    /// </summary>
    public class TimeSpanComparer : ValueComparer<TimeSpan>
    {
        /// <inheritdoc/>
        public TimeSpanComparer() : base(
            (ts1, ts2) => ts1.Ticks == ts2.Ticks,
            ts => ts.GetHashCode())
        { }
    }
}
