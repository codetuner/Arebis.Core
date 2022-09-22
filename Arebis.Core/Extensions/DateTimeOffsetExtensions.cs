using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        /// <summary>
        /// Returns the smallest (earliest) of both dates.
        /// If one of both dates is null, the other is returned.
        /// If both dates are null, null is returned.
        /// </summary>
        [return: NotNullIfNotNull("val1")]
        [return: NotNullIfNotNull("val2")]
        public static DateTimeOffset? Min(DateTimeOffset? val1, DateTimeOffset? val2)
        {
            if (val1.HasValue && val2.HasValue)
                return (val1 < val2) ? val1 : val2;
            else
                return val1 ?? val2;
        }

        /// <summary>
        /// Returns the larges (latest) of both dates.
        /// If one of both dates is null, the other is returned.
        /// If both dates are null, null is returned.
        /// </summary>
        [return: NotNullIfNotNull("val1")]
        [return: NotNullIfNotNull("val2")]
        public static DateTimeOffset? Max(DateTimeOffset? val1, DateTimeOffset? val2)
        {
            if (val1.HasValue && val2.HasValue)
                return (val1 > val2) ? val1 : val2;
            else
                return val1 ?? val2;
        }

        /// <summary>
        /// The age in full years. Time component is ignored.
        /// </summary>
        public static int AgeInYears(this DateTimeOffset dt, DateTimeOffset onDate)
        {
            var age = (onDate.Year - dt.Year);
            if (dt.Month > onDate.Month || (dt.Month == onDate.Month && dt.Day < onDate.Day)) age--;
            return age;
        }

        /// <summary>
        /// The age in full months. Time component is ignored.
        /// </summary>
        public static int AgeInMonths(this DateTimeOffset dt, DateTimeOffset onDate)
        {
            var age = (onDate.Year - dt.Year) * 12
            + (onDate.Month - dt.Month)
            + ((onDate.Day < dt.Day) ? -1 : 0);

            return age;
        }

        /// <summary>
        /// Whether the datetime is in the past.
        /// </summary>
        /// <param name="dt">The DateTimeOffset to evaluate.</param>
        public static bool IsInThePast(this DateTimeOffset dt)
        {
            return (dt.ToUniversalTime() < Current.DateTimeOffset.UtcNow);
        }

        /// <summary>
        /// Whether the datetime is in the future.
        /// </summary>
        /// <param name="dt">The DateTimeOffset to evaluate.</param>
        /// <param name="unspecifiedDefaultKind">When Kind is Unspecified, assume it's of this kind.</param>
        public static bool IsInTheFuture(this DateTimeOffset dt)
        {
            // As Current.DateTimeOffset is volatile, we do not consider dt == Current.DateTimeOffset as an 'InThePresent' case.
            // If it's not in the past, it's in the future...
            return !IsInThePast(dt);
        }
    }
}
