using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Data.SqlClient
{
    /// <summary>
    /// SqlException extension methods.
    /// </summary>
    public static class SqlExceptionExtensions
    {
        /// <summary>
        /// Whether this exception matches a SqlException marking a duplicate key violation (error number 2601 or 2627).
        /// </summary>
        public static bool IsSqlDuplicateKeyViolation(this Exception ex)
        {
            return ex is SqlException sqlex && (sqlex.HasErrorNumber(2601) || sqlex.HasErrorNumber(2627));
        }

        /// <summary>
        /// Whether this exception matches a SqlException marking a unique constraint violation (error number 2627).
        /// </summary>
        public static bool IsSqlUniqueConstraintViolation(this Exception ex)
        {
            return ex is SqlException sqlex && sqlex.HasErrorNumber(2627);
        }

        /// <summary>
        /// Whether this exception matches a SqlException marking a transient error that may be retried.
        /// </summary>
        public static bool IsSqlTransient(this Exception ex)
        {
            return ex is SqlException sqlex &&
                (sqlex.HasErrorNumber(4060)    // Cannot open database
                || sqlex.HasErrorNumber(10928) // Resource limit reached (Azure)
                || sqlex.HasErrorNumber(10929)
                || sqlex.HasErrorNumber(40197) // Azure transient
                || sqlex.HasErrorNumber(40501) // Service busy
                || sqlex.HasErrorNumber(40613) // Database unavailable
                || sqlex.HasErrorNumber(49918)
                || sqlex.HasErrorNumber(49919)
                || sqlex.HasErrorNumber(49920)
                || sqlex.HasErrorNumber(1205)  // Deadlock
                || sqlex.HasErrorNumber(-2));  // Timeout
        }

        /// <summary>
        /// Whether this SqlException contains an error with the given error number.
        /// </summary>
        public static bool HasErrorNumber(this SqlException ex, int errorNumber)
        {
            return ex.Errors.Cast<SqlError>().Any(error => error.Number == errorNumber);
        }
    }
}
