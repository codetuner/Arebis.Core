using Arebis.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Distributed;

namespace Arebis.Core.AspNet.SqlServer.DomainSettings
{
    /// <summary>
    /// A domain settings provider retrieving domain settings from a SQL Server database.
    /// </summary>
    public class SqlServerDomainSettingsProvider : IDomainSettingsProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly SqlServerDomainSettingsSource sqlServerDomainSettingsSource;
        private readonly IDistributedCache cache;

        /// <summary>
        /// Constructs a new SqlServerDomainSettingsProvider.
        /// </summary>
        public SqlServerDomainSettingsProvider(IHttpContextAccessor httpContextAccessor, SqlServerDomainSettingsSource sqlServerDomainSettingsSource, IDistributedCache cache)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.sqlServerDomainSettingsSource = sqlServerDomainSettingsSource;
            this.cache = cache;
        }

        /// <summary>
        /// Default timespan to cache domain settings.
        /// </summary>
        public static TimeSpan DefaultCachingTimespan { get; set; } = TimeSpan.FromMinutes(30);

        /// <inheritdoc/>
        public async Task<IDictionary<string, string?>> GetDomainSettingsAsync(CancellationToken ct = default)
        {
            return await this.GetDomainSettingsAsync(httpContextAccessor.HttpContext?.Request.Host.Value ?? string.Empty, ct);
        }

        internal async Task<IDictionary<string, string?>> GetDomainSettingsAsync(string domainName, CancellationToken ct)
        {
            IDictionary<string, string?>? settings= null;
            using (var connection = new SqlConnection(sqlServerDomainSettingsSource.ConnectionString))
            {
                using (var command = new SqlCommand("SELECT [AliasFor], [Key], [Value] FROM [config].[DomainSettings] WHERE [DomainName] = @domainName", connection))
                {
                    command.Parameters.Add("@domainName", System.Data.SqlDbType.VarChar, 200);

                    while (true)
                    {
                        // Try to get settings from cache:
                        settings = await this.cache.GetAsync<Dictionary<string, string?>>($"DomainSettings:{domainName.ToLowerInvariant()}");
                        if (settings == null)
                        {
                            // Retrieve settings from database:
                            if (connection.State != System.Data.ConnectionState.Open) connection.Open();
                            command.Parameters["@domainName"].Value = domainName;
                            settings = new Dictionary<string, string?>();
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (!reader.IsDBNull(0))
                                    {
                                        // This is an alias record, set alias and ignore all other settings (there shouldn't be any):
                                        settings.Clear();
                                        settings["__AliasFor"] = reader.GetString(0);
                                        break;
                                    }
                                    else if (!reader.IsDBNull(1))
                                    {
                                        // Add setting:
                                        var key = reader.GetString(1);
                                        var value = reader.IsDBNull(2) ? null : reader.GetString(2);
                                        settings[key] = value;
                                    }
                                }
                            }

                            // Store settings in cache:
                            await this.cache.SetAsync($"DomainSettings:{domainName.ToLowerInvariant()}", settings, DefaultCachingTimespan);
                        }

                        // If this is an alias, follow it:
                        if (settings.TryGetValue("__AliasFor", out var cachedAliasFor) && cachedAliasFor != null)
                        {
                            domainName = cachedAliasFor;
                            continue;
                        }
                        else
                        {
                            // Else return settings:
                            return settings;
                        }
                    }
                }
            }
        }

        /// <inheritdoc/>
        public async Task SetDomainAliasAsync(string domainName, string aliasFor, CancellationToken ct = default)
        {
            // Store alias in database:
            using (var connection = new SqlConnection(sqlServerDomainSettingsSource.ConnectionString))
            {
                if (connection.State != System.Data.ConnectionState.Open) connection.Open();
                var trans = await connection.BeginTransactionAsync();

                using (var command = new SqlCommand("DELETE FROM [config].[DomainSettings] WHERE [DomainName] = @domainName", connection))
                {
                    command.Parameters.AddWithValue("@domainName", domainName);
                    command.ExecuteNonQuery();
                }

                using (var command = new SqlCommand("INSERT INTO [config].[DomainSettings] ([DomainName], [AliasFor]) VALUES (@domainName, @aliasFor)", connection))
                {
                    command.Parameters.AddWithValue("@domainName", domainName);
                    command.Parameters.AddWithValue("@aliasFor", aliasFor);
                    command.ExecuteNonQuery();
                }

                await trans.CommitAsync();
            }

            // Store alias in cache:
            var settings = new Dictionary<string, string?>()
            {
                { "__AliasFor", aliasFor }
            };
            await this.cache.SetAsync($"DomainSettings:{domainName.ToLowerInvariant()}", settings, DefaultCachingTimespan);
        }

        /// <inheritdoc/>
        public async Task SetDomainSettingsAsync(string domainName, IDictionary<string, string?> settings, CancellationToken ct = default)
        {
            // Store settings in database:
            using (var connection = new SqlConnection(sqlServerDomainSettingsSource.ConnectionString))
            {
                if (connection.State != System.Data.ConnectionState.Open) connection.Open();
                var trans = await connection.BeginTransactionAsync();

                using (var command = new SqlCommand("DELETE FROM [config].[DomainSettings] WHERE [DomainName] = @domainName", connection))
                {
                    command.Parameters.AddWithValue("@domainName", domainName);
                    command.ExecuteNonQuery();
                }

                using (var command = new SqlCommand("INSERT INTO [config].[DomainSettings] ([DomainName], [Key], [Value]) VALUES (@domainName, @key, @value)", connection))
                {
                    command.Parameters.AddWithValue("@domainName", domainName);
                    command.Parameters.Add("@key", System.Data.SqlDbType.NVarChar, 200);
                    command.Parameters.Add("@value", System.Data.SqlDbType.NVarChar, -1);

                    foreach (var kvp in settings)
                    {
                        command.Parameters["@key"].Value = kvp.Key;
                        command.Parameters["@value"].Value = kvp.Value ?? (object)DBNull.Value;
                        command.ExecuteNonQuery();
                    }
                }

                await trans.CommitAsync();
            }

            // Store settings in cache:
            await this.cache.SetAsync($"DomainSettings:{domainName.ToLowerInvariant()}", settings, DefaultCachingTimespan);
        }

        /// <inheritdoc/>
        public async Task DeleteDomainAsync(string domainName, CancellationToken ct = default)
        {
            // Remove alias from database:
            using (var connection = new SqlConnection(sqlServerDomainSettingsSource.ConnectionString))
            {
                if (connection.State != System.Data.ConnectionState.Open) connection.Open();

                using (var command = new SqlCommand("DELETE FROM [config].[DomainSettings] WHERE [DomainName] = @domainName", connection))
                {
                    command.Parameters.AddWithValue("@domainName", domainName);
                    command.ExecuteNonQuery();
                }
            }

            // Remove alias from cache:
            await this.cache.RemoveAsync($"DomainSettings:{domainName.ToLowerInvariant()}");
        }
    }
}
