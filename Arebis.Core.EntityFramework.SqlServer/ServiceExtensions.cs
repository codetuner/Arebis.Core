using Arebis.Core.Data.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.EntityFramework.SqlServer
{
    /// <summary>
    /// Extensions to service registrations.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds a scoped DbContext using SqlServer and sharing connections through a registered SqlConnectionSource service.
        /// Use <code>builder.Services.AddScoped&lt;SqlConnectionSource&gt;();</code> to register a SqlConnectionSource.
        /// </summary>
        public static void AddScopedSqlDbContext<TDbContext> (this IServiceCollection services, string connectionName = "DefaultConnection", Action<DbContextOptionsBuilder<TDbContext>>? optionsAction = null, Action<SqlServerDbContextOptionsBuilder>? sqlServerOptionsAction = null)
            where TDbContext : DbContext
        {
            services.AddScoped<DbContextOptions<TDbContext>>(serviceProvider =>
            {
                var builder = new DbContextOptionsBuilder<TDbContext>();
                var source = serviceProvider.GetRequiredService<SqlConnectionSource>();
                var connection = source[connectionName];
                builder.UseSqlServer(connection, sqlServerOptionsAction);
                optionsAction?.Invoke(builder);

                return builder
                    .Options;
            });

            services.AddScoped<TDbContext>();
        }
    }
}
