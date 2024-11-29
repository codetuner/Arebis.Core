using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.AspNet.ServerSentEvents
{
    /// <summary>
    /// Server Sent Events (SSE) options.
    /// </summary>
    public class ServerSentEventsOptions
    {
        /// <summary>
        /// Time the client should wait beween reconnection attempts after a connection is lost.
        /// Defaults to 3 seconds.
        /// </summary>
        public TimeSpan? ReconnectionTime { get; set; } = null;
        
        /// <summary>
        /// Whether to recover on reconnection by trying to send all missed events.
        /// </summary>
        public bool RecoverOnReconnection { get; set; } = true;

        /// <summary>
        /// Optional path prefix for SSE connections.
        /// If not set, allows any path.
        /// </summary>
        public string? PathPrefix { get; set; } = null;

        /// <summary>
        /// Whether to accept any "Accept" header.
        /// By default, only "text/event-stream" Accept headers are accepted.
        /// If set to true, also set a PathPrefix to avoid all requests to be
        /// handled as SSE connection requests.
        /// </summary>
        public bool AcceptHeaderAny { get; internal set; } = false;

        /// <summary>
        /// Number of clients to keep if still within RetentionDuration.
        /// </summary>
        public int RetentionCount { get; set; } = 1000;

        /// <summary>
        /// Extra number of clients to keep to reduce retention cleanup frequency.
        /// </summary>
        public int RetentionOverrun { get; set; } = 100;

        /// <summary>
        /// How long data is kept about clients that have not received updated.
        /// </summary>
        public TimeSpan RetentionDuration { get; set; } = TimeSpan.FromMinutes(60);

        /// <summary>
        /// Idle timeout after which either a KeepAlive is sent or the connection
        /// is ended (expecting the client to reconnect if still alive).
        /// </summary>
        public TimeSpan IdleTimeout { get; set; } = TimeSpan.FromMinutes(5);

        /// <summary>
        /// Whether to send "$keepAlive" events on idle timeout.
        /// </summary>
        public bool SendKeepAlives { get; set; } = false;

        /// <summary>
        /// Whether to disconnect (expecting the client to reconnect) after each event or event batch.
        /// Guarantees that all events are immediately flushed to the client.
        /// </summary>
        public bool DisconnectAfterEachEvent { get; set; } = false;

        /// <summary>
        /// Schema name to use in case of database storage of client data.
        /// </summary>
        public string? DatabaseSchema { get; set; } = null;

        /// <summary>
        /// Invoked to determine if the connection request is a Server Sent Events connection.
        /// </summary>
        public Func<HttpContext, bool>? IsSseConnection = null;

        /// <summary>
        /// Invoked when an incomming connection is identified as an SSE connection request.
        /// </summary>
        public Action<HttpContext>? OnIncomingConnection = null;
        
        /// <summary>
        /// Invoked when an incomming connection has lead to the creation of a new proxy.
        /// </summary>
        public Action<HttpContext, ServerSentEventsClientProxy>? OnProxyCreated = null;
        
        /// <summary>
        /// Invoked when a connection is dropped and it's proxy is to be deleted.
        /// </summary>
        public Action<HttpContext, ServerSentEventsClientProxy>? OnProxyDeleted = null;
    }
}
