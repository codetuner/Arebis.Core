using Microsoft.Extensions.Configuration;

namespace Arebis.Core.Configuration.SqlServer
{
    // Source: https://mousavi310.github.io/posts/a-refreshable-sql-server-configuration-provider-for-net-core/

    /// <summary>
    /// Configuration builder extensions for SQL Server configuration source.
    /// </summary>
    public static class SqlServerConfigurationBuilderExtensions
    {
        /// <summary>
        /// Adds the SQL Server configuration source to the configuration builder.
        /// </summary>
        public static IConfigurationBuilder AddSqlServer(this IConfigurationBuilder builder, string connectionString, string? environment = null, TimeSpan? refreshInterval = null)
        {
            return builder.Add(new SqlServerConfigurationSource
            {
                ConnectionString = connectionString,
                Environment = environment ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production",
                SqlServerWatcher = refreshInterval.HasValue ? new SqlServerPeriodicalWatcher(refreshInterval.Value) : null
            });
        }
    }
}
