using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core
{
    /// <summary>
    /// Assertion methods to ease validation in business methods.
    /// The class is named LiveAssert to avoid conflicts with the UnitTesting Assert methods,
    /// and because it asserts during 'live' execution.
    /// </summary>
    public static class LiveAssert
    {
        /// <summary>
        /// Throws an ArgumentNullException if value is null.
        /// </summary>
        public static void ArgumentNotNull([NotNull] object? value, [CallerArgumentExpression("value")] string? argName = null)
        {
            if (value is null) throw new ArgumentNullException(argName);
        }

        /// <summary>
        /// Throws an ArgumentNullException if value is null.
        /// </summary>
        public static void ArgumentNotNullOrEmpty([NotNull] string? value, [CallerArgumentExpression("value")] string? argName = null)
        {
            if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(argName);
        }

        /// <summary>
        /// Throws an ArgumentOutOfRangeException if value is out of range.
        /// </summary>
        public static void ArgumentNotLessThan(int value, int minimumAllowedValue, [CallerArgumentExpression("value")] string? argName = null)
        {
            if (value < minimumAllowedValue)
                throw new ArgumentOutOfRangeException(argName, value, String.Format("Value should not be less than {0}.", minimumAllowedValue));
        }

        /// <summary>
        /// Throws an ArgumentOutOfRangeException if value is out of range.
        /// </summary>
        public static void ArgumentNotLargerThan(int value, int maximumAllowedValue, [CallerArgumentExpression("value")] string? argName = null)
        {
            if (value > maximumAllowedValue)
                throw new ArgumentOutOfRangeException(argName, value, String.Format("Value should not be larger than {0}.", maximumAllowedValue));
        }

        /// <summary>
        /// Throws an ArgumentOutOfRangeException if value is out of range.
        /// </summary>
        public static void ArgumentBetween(int value, int minimumAllowedValue, int maximumAllowedValue, [CallerArgumentExpression("value")] string? argName = null)
        {
            if (value < minimumAllowedValue)
                throw new ArgumentOutOfRangeException(argName, value, String.Format("Value should not be less than {0}.", minimumAllowedValue));
            if (value > maximumAllowedValue)
                throw new ArgumentOutOfRangeException(argName, value, String.Format("Value should not be larger than {0}.", maximumAllowedValue));
        }

        /// <summary>
        /// Throws an ArgumentException if condition is not met.
        /// </summary>
        public static void ArgumentPrecondition(string argName, bool condition, string? messageOnFailure = null)
        {
            if (condition == false)
                throw new ArgumentException(messageOnFailure ?? "Argument does not meet preconditions.", argName);
        }

        /// <summary>
        /// Throws an InvalidOperationException if condition is not met.
        /// </summary>
        public static void Precondition(bool condition, string? messageOnFailure = null)
        {
            if (condition == false)
                throw new InvalidOperationException(messageOnFailure ?? "Argument does not meet preconditions.");
        }

        /// <summary>
        /// Throw an instance of the given TException if condition is not met.
        /// </summary>
        public static void Custom<TException>(bool condition)
            where TException : Exception, new()
        {
            if (condition == false)
                throw new TException();
        }
    }
}
