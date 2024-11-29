using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.AspNet.ServerSentEvents
{
    /// <summary>
    /// The <see cref="ServerSentEventsService"/> manages proxies to Server Sent Events clients.
    /// </summary>
    public abstract class ServerSentEventsService
    {
        #region Static message "constants"

        /// <summary>
        /// Welcome message.
        /// </summary>
        public static string WelcomeMessage = "$welcome";

        /// <summary>
        /// Welcome message data.
        /// </summary>
        public static string WelcomeMessageData = "{\"version\":\"1.0\"}";

        /// <summary>
        /// KeepAlive message.
        /// </summary>
        public static string KeepAliveMessage = "$keepAlive";

        /// <summary>
        /// KeepAlive message data.
        /// </summary>
        public static string KeepAliveMessageData = "null";

        #endregion

        /// <summary>
        /// Connects or reconnects a client.
        /// </summary>
        /// <param name="context">Request HttpContext.</param>
        /// <param name="recoverOnReconnection">Whether to send missed events on reconnection.</param>
        public abstract Task<ServerSentEventsClientProxy> ConnectClient(HttpContext context, bool recoverOnReconnection);

        /// <summary>
        /// Disconnects a cient.
        /// </summary>
        public abstract Task DisconnectClient(HttpContext context, ServerSentEventsClientProxy clientProxy);

        /// <summary>
        /// Returns the number of client proxies currently held by the <see cref="ServerSentEventsService"/>.
        /// </summary>
        /// <returns></returns>
        public abstract Task<int> GetClientCount();
    }

    /// <inheritdoc/>
    public class ServerSentEventsService<TCdo> : ServerSentEventsService
        where TCdo : ServerSentEventsClientData, new()
    {
        private readonly ConcurrentDictionary<Guid, ServerSentEventsClientProxy> proxies = [];
        private readonly IServerSentEventsClientsDataStore<TCdo> dataStore;
        private readonly IOptions<ServerSentEventsOptions> options;
        private readonly ILogger<ServerSentEventsService<TCdo>> logger;
        private readonly ILogger<ServerSentEventsClientProxy> proxyLogger;

        /// <summary>
        /// Constructs an instance of <see cref="ServerSentEventsService{TCdo}"/>.
        /// </summary>
        public ServerSentEventsService(IServerSentEventsClientsDataStore<TCdo> dataStore, IOptions<ServerSentEventsOptions> options, ILogger<ServerSentEventsService<TCdo>> logger, ILogger<ServerSentEventsClientProxy> proxyLogger)
        {
            this.dataStore = dataStore;
            this.options = options;
            this.logger = logger;
            this.proxyLogger = proxyLogger;
            dataStore.EventQueueing += async (s, e) =>
            {
                // Async EventHandlers, see:
                // https://www.codeproject.com/Articles/5354588/Async-EventHandlers-A-Simple-Safety-Net-to-the-Res
                try
                {
                    if (proxies.TryGetValue(e.EventQueueing.ClientIdentifier!.Value, out var proxy))
                    {
                        await proxy.SendEventAsync(e.EventQueueing);
                        e.EventQueueing.IsSent = true;
                    }
                }
                catch (Exception) { }
            };
        }

        /// <inheritdoc/>
        public override async Task<ServerSentEventsClientProxy> ConnectClient(HttpContext context, bool recoverOnReconnection)
        {
            ServerSentEventsClientProxy clientProxy;

            if (context.Request.Headers.ContainsKey("Last-Event-ID"))
            {
                // Parse lastEventId:
                var parts = context.Request.Headers["Last-Event-ID"].ToString().Split(':');
                if (parts.Length == 2)
                {
                    // Search for already registered client:
                    var identifier = Guid.Parse(parts[0]);
                    var lastId = Int32.Parse(parts[1]);

                    // Get a client proxy instance:
                    if (proxies.TryGetValue(identifier, out ServerSentEventsClientProxy? client))
                    {
                        logger.LogInformation("Matching {identifier} on existing proxy.", identifier);
                        clientProxy = client;
                    }
                    else
                    {
                        logger.LogInformation("Matching {identifier} to new proxy.", identifier);
                        clientProxy = new ServerSentEventsClientProxy(identifier, context.Response, context.RequestAborted, proxyLogger);
                        proxies[identifier] = clientProxy;
                        options.Value.OnProxyCreated?.Invoke(context, clientProxy);
                    }

                    // Retrieve the data for the client (if exists):
                    TCdo? clientData;
                    if (dataStore.TryGetClientData(identifier, out clientData))
                    { 
                        if (recoverOnReconnection)
                        {
                            var events = dataStore.ListEvents(identifier, lastId);
                            await clientProxy.RecoverFromAsync(events);
                        }
                    }
                    else
                    {
                        // Create and initialize new data object:
                        clientData = NewDataObject(identifier, context);
                        clientData.OnInitialize(context);

                        // Store data object:
                        await dataStore.StoreClientData(clientData, context);
                    }
                }
                else
                {
                    throw new ArgumentException($"Value for \"Last-Event-ID\" request header value \"{context.Request.Headers["Last-Event-ID"]}\" has an unsupported format.");
                }
            }
            else
            {
                // It's a brand new client; create a new proxy:
                clientProxy = new ServerSentEventsClientProxy(Guid.NewGuid(), context.Response, context.RequestAborted, proxyLogger);
                proxies[clientProxy.Identifier] = clientProxy;
                logger.LogInformation("Created new proxy with identifier {identifier}.", clientProxy.Identifier);

                // Create and initialize new data object:
                var clientData = NewDataObject(clientProxy.Identifier, context);
                clientData.OnInitialize(context);

                // Store data object:
                await dataStore.StoreClientData(clientData, context);

                // Inform client about connection:
                await clientProxy.WriteAsync(ServerSentEventsService.WelcomeMessage, ServerSentEventsService.WelcomeMessageData, 0, context.RequestAborted);
            }

            return clientProxy;
        }

        /// <inheritdoc/>
        public override Task DisconnectClient(HttpContext context, ServerSentEventsClientProxy clientProxy)
        {
            logger.LogInformation("Disconnecting proxy with identifier {identifier}.", clientProxy.Identifier);
            options.Value.OnProxyDeleted?.Invoke(context, clientProxy);
            this.proxies.TryRemove(clientProxy.Identifier, out var c);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public override Task<int> GetClientCount()
        {
            return Task.FromResult(proxies.Count);
        }

        #region Internal implementation

        private TCdo NewDataObject(Guid forIdentifier, HttpContext context)
        {
            // Create client data object:
            var cdo = new TCdo();
            cdo.Identifier = forIdentifier;
            cdo.Path = context.Request.Path;

            // Copy query values to client data object properties:
            foreach (var value in context.Request.Query)
            {
                SetProperty(cdo, value.Key, value.Value);
            }

            // Return it:
            return cdo;
        }

        private void SetProperty(TCdo cdo, string name, string? value)
        {
            var property = typeof(TCdo).GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.IgnoreCase);
            if (property != null)
            {
                if (value == null)
                {
                    property.SetValue(cdo, null);
                }
                else if (property.PropertyType.IsArray)
                {
                    // Don't throw, we OnInitialize method could still be used:
                    //throw new InvalidOperationException("Array properties are currently not yet supported on ClientData types.");
                }
                else
                {
                    property.SetValue(cdo, Convert.ChangeType(value, property.PropertyType));
                }
            }
        }

        #endregion
    }
}
