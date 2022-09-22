using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace Arebis.Core.Extensions
{
	/// <summary>
	/// Provides extension methods to the DateTime struct.
	/// </summary>
	/// <remarks>
	/// As Extension Methods will be supported by the C# language, these
	/// methods will be changed into real Extension Methods.
	/// </remarks>
	public static class DateTimeExtension
	{
        /// <summary>
        /// Returns the smallest (earliest) of both dates.
        /// If one of both dates is null, the other is returned.
        /// If both dates are null, null is returned.
        /// </summary>
        [return: NotNullIfNotNull("val1")]
        [return: NotNullIfNotNull("val2")]
        public static DateTime? Min(DateTime? val1, DateTime? val2)
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
        public static DateTime? Max(DateTime? val1, DateTime? val2)
        {
            if (val1.HasValue && val2.HasValue)
                return (val1 > val2) ? val1 : val2;
            else
                return val1 ?? val2;
        }

        /// <summary>
        /// Returns the given DateTime as Excel numerical value.
        /// That is, the number of days since 1900/01/01 00:00 (which has value 1.0).
        /// </summary>
        public static double ToExcelTime(this DateTime dt)
        {
            TimeSpan t = dt - new DateTime(1899, 12, 31);
            return t.TotalDays;
        }

        /// <summary>
        /// The age in full years. Time component is ignored.
        /// </summary>
        public static int AgeInYears(this DateTime dt, DateTime onDate)
        {
            var age = (onDate.Year - dt.Year);
            if (dt.Month > onDate.Month || (dt.Month == onDate.Month && dt.Day < onDate.Day)) age--;
            return age;
        }

        /// <summary>
        /// The age in full months. Time component is ignored.
        /// </summary>
        public static int AgeInMonths(this DateTime dt, DateTime onDate)
        {
            var age = (onDate.Year - dt.Year) * 12
            + (onDate.Month - dt.Month)
            + ((onDate.Day < dt.Day) ? -1 : 0);

            return age;
        }

        /// <summary>
        /// Whether the datetime is in the past.
        /// </summary>
        /// <param name="dt">The DateTime to evaluate.</param>
        /// <param name="unspecifiedDefaultKind">When Kind is Unspecified, assume it's of this kind.</param>
        public static bool IsInThePast(this DateTime dt, DateTimeKind unspecifiedDefaultKind = DateTimeKind.Local)
        {
            if (dt.Kind == DateTimeKind.Utc || (dt.Kind == DateTimeKind.Unspecified && unspecifiedDefaultKind == DateTimeKind.Utc))
                return (DateTime.SpecifyKind(dt, DateTimeKind.Utc) < Current.DateTime.UtcNow);
            else
                return (dt < Current.DateTime.Now);
        }

        /// <summary>
        /// Whether the datetime is in the future.
        /// </summary>
        /// <param name="dt">The DateTime to evaluate.</param>
        /// <param name="unspecifiedDefaultKind">When Kind is Unspecified, assume it's of this kind.</param>
        public static bool IsInTheFuture(this DateTime dt, DateTimeKind unspecifiedDefaultKind = DateTimeKind.Local)
        {
            // As Current.DateTime is volatile, we do not consider dt == Current.DateTime as an 'InThePresent' case.
            // If it's not in the past, it's in the future...
            return !IsInThePast(dt, unspecifiedDefaultKind);
        }
    }
}
