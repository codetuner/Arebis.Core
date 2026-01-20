using Microsoft.Extensions.DependencyInjection;

namespace Arebis.Core.AspNet.SqlServer.DomainSettings
{
    /// <summary>
    /// Sql Server domain settings extensions for IServiceCollection.
    /// </summary>
    public static class SqlServerDomainSettingsExtensions
    {
        /// <summary>
        /// Adds the SQL Server domain settings provider.
        /// </summary>
        public static IServiceCollection AddSqlServerDomainSettingsProvider(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton(new SqlServerDomainSettingsSource(connectionString));
            services.AddScoped<IDomainSettingsProvider, SqlServerDomainSettingsProvider>();
            return services;
        }
    }
}
