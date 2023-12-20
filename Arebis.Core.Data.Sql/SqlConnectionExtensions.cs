using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Data.Sql
{
    /// <summary>
    /// SqlConnection extension methods.
    /// </summary>
    public static class SqlConnectionExtensions
    {
        /// <summary>
        /// Returns the last generated identity of this scope (SCOPE_IDENTITY).
        /// </summary>
        public static long? GetLastGeneratedIdentity(this SqlConnection connection)
        {
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                return (long?)cmd.ExecuteScalar();
            }
        }
    }
}
