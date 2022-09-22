using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Extensions
{
    public static class DateOnlyExtensions
    {
        /// <summary>
        /// Returns the smallest (earliest) of both dates.
        /// If one of both dates is null, the other is returned.
        /// If both dates are null, null is returned.
        /// </summary>
        [return: NotNullIfNotNull("val1")]
        [return: NotNullIfNotNull("val2")]
        public static DateOnly? Min(DateOnly? val1, DateOnly? val2)
        {
            if (val1.HasValue && val2.HasValue)
            {
                return (val1 < val2) ? val1 : val2;
            }
            else
            {
                return val1 ?? val2;
            }
        }

        /// <summary>
        /// Returns the larges (latest) of both dates.
        /// If one of both dates is null, the other is returned.
        /// If both dates are null, null is returned.
        /// </summary>
        [return: NotNullIfNotNull("val1")]
        [return: NotNullIfNotNull("val2")]
        public static DateOnly? Max(DateOnly? val1, DateOnly? val2)
        {
            if (val1.HasValue && val2.HasValue)
            {
                return (val1 > val2) ? val1 : val2;
            }
            else
            {
                return val1 ?? val2;
            }
        }

        /// <summary>
        /// The age in full years.
        /// </summary>
        public static int AgeInYears(this DateOnly dt, DateOnly onDate)
        {
            var age = (onDate.Year - dt.Year);
            if (dt.Month > onDate.Month || (dt.Month == onDate.Month && dt.Day < onDate.Day)) age--;
            return age;
        }

        /// <summary>
        /// The age in full months.
        /// </summary>
        public static int AgeInMonths(this DateOnly dt, DateOnly onDate)
        {
            var age = (onDate.Year - dt.Year) * 12
            + (onDate.Month - dt.Month)
            + ((onDate.Day < dt.Day) ? -1 : 0);

            return age;
        }
    }
}
