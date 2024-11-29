using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Arebis.Core.AspNet.ServerSentEvents
{
    /// <summary>
    /// A proxy to a Server Sent Events client.
    /// </summary>
    public class ServerSentEventsClientProxy
    {
        private readonly ILogger<ServerSentEventsClientProxy> logger;

        /// <summary>
        /// Constructs a <see cref="ServerSentEventsClientProxy"/> instance.
        /// </summary>
        public ServerSentEventsClientProxy(Guid identifier, HttpResponse response, CancellationToken cancellationToken, ILogger<ServerSentEventsClientProxy>? logger)
        {
            AutoResetEvent = new AutoResetEvent(false);
            Identifier = identifier;
            Response = response;
            CancellationToken = cancellationToken;
            this.logger = logger ?? NullLogger<ServerSentEventsClientProxy>.Instance;
        }

        /// <summary>
        /// Unique identifier of this client.
        /// </summary>
        public Guid Identifier { get; }

        /// <summary>
        /// HTTP response object connecting to the client.
        /// </summary>
        public HttpResponse Response { get; }

        /// <summary>
        /// CancellationToken related to the clients connection.
        /// </summary>
        public CancellationToken CancellationToken { get; }

        /// <summary>
        /// AutoResetEvent set whenever the proxy is accessed.
        /// </summary>
        public AutoResetEvent AutoResetEvent { get; }

        /// <summary>
        /// Custom data to relate to the proxy.
        /// </summary>
        public Dictionary<string, object> Data { get; } = [];

        /// <summary>
        /// Sets the reconnection time. By default 3 seconds.
        /// </summary>
        public async Task SetReconnectionTime(TimeSpan reconnectionTime)
        {
            try
            {
                if (this.CancellationToken.IsCancellationRequested) return;
                await this.Response.WriteAsync($"retry: {(long)reconnectionTime.TotalMilliseconds}\n\n", this.CancellationToken);
                await this.Response.Body.FlushAsync(this.CancellationToken);
            }
            catch (OperationCanceledException)
            { }
        }

        /// <summary>
        /// Recovers from a connection loss with thiven last Id.
        /// </summary>
        public async Task RecoverFromAsync(IEnumerable<ServerSentEvent> events)
        {
            try
            {
                logger.LogInformation("Recovering proxy {identifier}.", this.Identifier);
                var hasSentEvents = false;
                if (this.CancellationToken.IsCancellationRequested) return;
                foreach (var @event in events)
                {
                    if (@event.ExpiryTime.HasValue && @event.ExpiryTime.Value.ToUniversalTime() < DateTime.UtcNow) continue;
                    await WriteAsync(@event.Type, @event.Data, @event.Id, this.CancellationToken);
                    @event.IsSent = true;
                    hasSentEvents = true;
                }
                if (hasSentEvents)
                {
                    await this.Response.Body.FlushAsync(this.CancellationToken);
                    this.AutoResetEvent.Set();
                }
            }
            catch (OperationCanceledException)
            { }
        }

        /// <summary>
        /// Sends an event to the client. The event is lost if the client is not available.
        /// </summary>
        public async Task SendEventAsync(ServerSentEvent @event)
        {
            try
            {
                logger.LogInformation("Sending event to proxy {identifier}.", this.Identifier);
                if (this.CancellationToken.IsCancellationRequested) return;
                await this.WriteAsync(@event.Type, @event.Data, @event.Id, this.CancellationToken);
                await this.Response.Body.FlushAsync(this.CancellationToken);
                @event.IsSent = true;
                this.AutoResetEvent.Set();
            }
            catch (OperationCanceledException)
            { }
        }

        /// <summary>
        /// Send a Keep-Alive event.
        /// </summary>
        public async Task SendKeepAlive()
        {
            try
            {
                if (this.CancellationToken.IsCancellationRequested) return;
                await this.Response.WriteAsync($"event: {ServerSentEventsService.KeepAliveMessage}\ndata: {ServerSentEventsService.KeepAliveMessageData}\n\n", this.CancellationToken);
                await this.Response.Body.FlushAsync(this.CancellationToken);
            }
            catch (OperationCanceledException)
            { }
        }

        /// <summary>
        /// Internal write to response implementation.
        /// </summary>
        internal async Task WriteAsync(string @event, string data, int id, CancellationToken ct)
        {
            logger.LogDebug("Writing to \"{identifer}\": id \"{id}\", event \"{event}\", data \"{data}\".", this.Identifier, id, @event, data);

            await this.Response.WriteAsync($"id: {this.Identifier}:{id:000000000}\n", ct);
            await this.Response.WriteAsync($"event: {@event}\n", ct);
            foreach (var line in data.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n'))
            {
                await this.Response.WriteAsync($"data: {line}\n", ct);
            }
            await this.Response.WriteAsync($"\n", ct);
        }
    }
}
