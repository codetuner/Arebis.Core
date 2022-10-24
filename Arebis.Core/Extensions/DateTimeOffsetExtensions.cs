using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Extensions
{
    /// <summary>
    /// DateTimeOffset extension methods.
    /// </summary>
    public static class DateTimeOffsetExtensions
    {
        /// <summary>
        /// Returns the smallest (earliest) of both dates.
        /// If one of both dates is null, the other is returned.
        /// If both dates are null, null is returned.
        /// </summary>
        [return: NotNullIfNotNull("dto1")]
        [return: NotNullIfNotNull("dto2")]
        public static DateTimeOffset? Min(DateTimeOffset? dto1, DateTimeOffset? dto2)
        {
            if (dto1.HasValue && dto2.HasValue)
                return (dto1 < dto2) ? dto1 : dto2;
            else
                return dto1 ?? dto2;
        }

        /// <summary>
        /// Returns the larges (latest) of both dates.
        /// If one of both dates is null, the other is returned.
        /// If both dates are null, null is returned.
        /// </summary>
        [return: NotNullIfNotNull("dto1")]
        [return: NotNullIfNotNull("dto2")]
        public static DateTimeOffset? Max(DateTimeOffset? dto1, DateTimeOffset? dto2)
        {
            if (dto1.HasValue && dto2.HasValue)
                return (dto1 > dto2) ? dto1 : dto2;
            else
                return dto1 ?? dto2;
        }

        /// <summary>
        /// The age in full years. Time component is ignored.
        /// </summary>
        public static int AgeInYears(this DateTimeOffset dto, DateTimeOffset onDate)
        {
            var age = (onDate.Year - dto.Year);
            if (dto.Month > onDate.Month || (dto.Month == onDate.Month && dto.Day < onDate.Day)) age--;
            return age;
        }

        /// <summary>
        /// The age in full months. Time component is ignored.
        /// </summary>
        public static int AgeInMonths(this DateTimeOffset dto, DateTimeOffset onDate)
        {
            var age = (onDate.Year - dto.Year) * 12
            + (onDate.Month - dto.Month)
            + ((onDate.Day < dto.Day) ? -1 : 0);

            return age;
        }

        /// <summary>
        /// Whether the datetime is in the past.
        /// </summary>
        /// <param name="dto">The DateTimeOffset to evaluate.</param>
        public static bool IsInThePast(this DateTimeOffset dto)
        {
            return (dto.ToUniversalTime() < Current.DateTimeOffset.UtcNow);
        }

        /// <summary>
        /// Whether the datetime is in the future.
        /// </summary>
        /// <param name="dto">The DateTimeOffset to evaluate.</param>
        public static bool IsInTheFuture(this DateTimeOffset dto)
        {
            // As Current.DateTimeOffset is volatile, we do not consider dt == Current.DateTimeOffset as an 'InThePresent' case.
            // If it's not in the past, it's in the future...
            return !IsInThePast(dto);
        }
    }
}
