﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Data
{
    /// <summary>
    /// DbCommand extension methods.
    /// </summary>
    public static class DbCommandExtensions
    {
        /// <summary>
        /// Adds a parameter to the command.
        /// </summary>
        public static DbCommand AddParameter(this DbCommand cmd, string name, object? value, ParameterDirection direction = ParameterDirection.Input, DbType? dbType = null, int? size = null)
        {
            var param = cmd.CreateParameter();
            param.ParameterName = name;
            param.Value = value ?? DBNull.Value;
            param.Direction = direction;
            if (dbType.HasValue) param.DbType = dbType.Value;
            if (size.HasValue) param.Size = size.Value;
            cmd.Parameters.Add(param);
            return cmd;
        }

        /// <summary>
        /// Executes the query, and returns the first row in the resultset returned by the query. Extra rows are ignored.
        /// Return null if no (first) row(s).
        /// </summary>
        public static object[]? ExecuteSingleRow(this IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var result = new object[reader.FieldCount];
                    reader.GetValues(result);
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
