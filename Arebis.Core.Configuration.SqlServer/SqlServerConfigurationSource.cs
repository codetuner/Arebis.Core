using Microsoft.Extensions.Configuration;

namespace Arebis.Core.Configuration.SqlServer
{
    // Source: https://mousavi310.github.io/posts/a-refreshable-sql-server-configuration-provider-for-net-core/

    /// <summary>
    /// A SQL Server configuration source.
    /// </summary>
    public class SqlServerConfigurationSource : IConfigurationSource
    {
        /// <summary>
        /// The connection string to the SQL Server database.
        /// </summary>
        public required string ConnectionString { get; set; }

        /// <summary>
        /// The application environment (e.g., Development, Staging, Production).
        /// </summary>
        public required string Environment { get; set; }

        /// <summary>
        /// An optional SQL Server watcher for detecting configuration changes.
        /// </summary>
        public required ISqlServerWatcher? SqlServerWatcher { get; set; }

        /// <summary>
        /// Builds a new <see cref="IConfigurationProvider"/> for loading configuration values from a SQL Server
        /// database.
        /// </summary>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new SqlServerConfigurationProvider(this);
        }
    }
}
