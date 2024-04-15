using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace Arebis.Core.Data.Sql
{
    /// <summary>
    /// Injectable service to provide SqlConnections from configuration.
    /// </summary>
    public class SqlConnectionSource : IDisposable
    {
        private readonly IConfiguration configuration;
        private readonly Dictionary<string, SqlConnection> connections = new();

        /// <summary>
        /// Constructs a SqlConnectionSource.
        /// </summary>
        public SqlConnectionSource(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Returns the SqlConnection with the given configuration name, or create one.
        /// </summary>
        public SqlConnection this[string name]
        {
            get
            {
                if (!connections.TryGetValue(name, out SqlConnection? conn))
                {
                    var cs = this.configuration.GetConnectionString(name);
                    return connections[name] = new SqlConnection(cs);
                }
                else
                {
                    return conn;
                }
            }
        }

        /// <summary>
        /// Disposes all connections.
        /// </summary>
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
            foreach (var connection in connections.Values)
                connection.Dispose();
        }
    }
}
