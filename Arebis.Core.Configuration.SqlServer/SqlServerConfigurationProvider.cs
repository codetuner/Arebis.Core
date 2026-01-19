using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Arebis.Core.Configuration.SqlServer
{
    // Source: https://mousavi310.github.io/posts/a-refreshable-sql-server-configuration-provider-for-net-core/

    /// <summary>
    /// Sql Server configuration provider.
    /// </summary>
    public class SqlServerConfigurationProvider : ConfigurationProvider
    {
        private readonly SqlServerConfigurationSource source;
        private const string Query = "SELECT [Key], [Value], 0 FROM [config].[AppConfiguration] WHERE [Environment] IS NULL UNION SELECT [Key], [Value], 1 FROM [config].[AppConfiguration] WHERE [Environment] = @environment ORDER BY 3";

        /// <summary>
        /// Sql Server configuration provider constructor.
        /// </summary>
        public SqlServerConfigurationProvider(SqlServerConfigurationSource source)
        {
            this.source = source;

            if (source.SqlServerWatcher != null)
            {
                /*changeTokenRegistration = */ChangeToken.OnChange(
                    () => source.SqlServerWatcher.Watch(),
                    Load
                );
            }
        }

        /// <inheritdoc/>
        public override void Load()
        {
            var dic = new Dictionary<string, string?>();
            using (var connection = new SqlConnection(source.ConnectionString))
            {
                var query = new SqlCommand(Query, connection);
                query.Parameters.AddWithValue("@environment", source.Environment);

                query.Connection.Open();

                using (var reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dic[(string)reader[0]] = (reader[1] == DBNull.Value)
                            ? null
                            : (string)reader[1];
                    }
                }
            }

            Data = dic;
        }
    }
}
