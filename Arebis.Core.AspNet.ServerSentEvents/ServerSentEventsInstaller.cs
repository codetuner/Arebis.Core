using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.AspNet.ServerSentEvents
{
    /// <summary>
    /// Installer methods for Server Sent Events support.
    /// </summary>
    public static class ServerSentEventsInstaller
    {
        /// <summary>
        /// Installs the services required for Server Sent Events.
        /// If no other <see cref="IServerSentEventsClientsDataStore{TCdo}"/> is installed,
        /// a <see cref="MemoryServerSentEventsClientsDataStore{TCdo}"/> is installed.
        /// </summary>
        /// <typeparam name="TServerSentEventsClientData">Subtype of <see cref="ServerSentEventsClientData"/> holding the data to associate with SSE clients.</typeparam>
        public static IServiceCollection AddServerSentEvents<TServerSentEventsClientData>(this IServiceCollection services, IConfiguration configuration, Action<ServerSentEventsOptions>? optionsAction = null)
            where TServerSentEventsClientData : ServerSentEventsClientData, new()
        {
            services.Configure<ServerSentEventsOptions>(options => {
                configuration.GetSection("ServerSentEvents").Bind(options);
                optionsAction?.Invoke(options);
            });

            services.TryAddSingleton<IServerSentEventsClientsDataStore<TServerSentEventsClientData>, MemoryServerSentEventsClientsDataStore<TServerSentEventsClientData>>();
            services.TryAddSingleton<ServerSentEventsService, ServerSentEventsService<TServerSentEventsClientData>>();

            return services;
        }

        /// <summary>
        /// Installs middleware for Server Sent Events.
        /// </summary>
        public static IApplicationBuilder UseServerSentEvents(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ServerSentEventsMiddleware>();
        }
    }
}
