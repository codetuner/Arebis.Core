using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Extensions
{
    /// <summary>
    /// TimeSpan extension methods.
    /// </summary>
    public static class TimeSpanExtension
    {
        /// <summary>
        /// Returns the absolute value of the timestamp (an anways positive value).
        /// </summary>
        public static TimeSpan Absolute(this TimeSpan ts)
        {
            return (ts.Ticks >= 0) ? ts : ts.Negate();
        }
    }
}
