using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.AspNet.ServerSentEvents
{
    /// <summary>
    /// Represents a single Server Sent Event sent or to be sent to a client.
    /// </summary>
    public class ServerSentEvent
    {
        /// <summary>
        /// If of the event.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identifier of the client.
        /// </summary>
        public Guid? ClientIdentifier { get; set; }

        /// <summary>
        /// Type of the event.
        /// </summary>
        public required string Type { get; set; }

        /// <summary>
        /// Data of the event.
        /// </summary>
        public required string Data { get; set; }

        /// <summary>
        /// Optional expiray time after which the event will not be sent anymore.
        /// </summary>
        public DateTime? ExpiryTime { get; set; }

        /// <summary>
        /// Whether the event has successfully been sent.
        /// </summary>
        public bool IsSent { get; set; }

        /// <summary>
        /// Clones this event.
        /// </summary>
        public ServerSentEvent Clone()
        {
            return (ServerSentEvent)this.MemberwiseClone();
        }
    }
}
