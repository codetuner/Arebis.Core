using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Data
{
    /// <summary>
    /// DbConnection extension methods.
    /// </summary>
    public static class DbConnectionExtensions
    {
        /// <summary>
        /// Opens the connection if it is not currently open.
        /// </summary>
        public static void EnsureOpen(this IDbConnection conn)
        {
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
        }

        /// <summary>
        /// Opens the connection if it is not currently open.
        /// </summary>
        public static async Task EnsureOpenAsync(this DbConnection conn)
        {
            if (conn.State != System.Data.ConnectionState.Open) await conn.OpenAsync();
        }

        /// <summary>
        /// Creates a DbCommand for this connection.
        /// </summary>
        public static DbCommand CreateCommand(this DbConnection conn, string commandText, CommandType commandType = CommandType.Text, DbTransaction? transaction = null)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            cmd.Transaction = transaction;
            return cmd;
        }
    }
}
