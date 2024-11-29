using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.AspNet.ServerSentEvents
{
    /// <summary>
    /// Data for a ServerSentEventsClient.
    /// Inherit from this class and add additional properties to identify
    /// and select clients to queue events to.
    /// </summary>
    public abstract class ServerSentEventsClientData
    {
        /// <summary>
        /// Constructs a new <see cref="ServerSentEventsClientData"/> instance.
        /// </summary>
        public ServerSentEventsClientData()
        {
            this.Path = null!;
            this.LastUsedId = 0;
            this.Events = new List<ServerSentEvent>();
        }

        /// <summary>
        /// Unique identifier of this client.
        /// </summary>
        public Guid Identifier { get; set; }

        /// <summary>
        /// Path of the SSE handler.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Last used Id for this client.
        /// </summary>
        public int LastUsedId { get; set; }

        /// <summary>
        /// Queue of events sent to this client.
        /// </summary>
        public IList<ServerSentEvent> Events { get; set; }

        /// <summary>
        /// Invoked on initialization of new client data.
        /// </summary>
        /// <remarks>Base implementation is empty and does not need to be called.</remarks>
        public virtual void OnInitialize(HttpContext context)
        { }
    }
}
