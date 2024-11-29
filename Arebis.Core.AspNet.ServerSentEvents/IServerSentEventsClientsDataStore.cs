using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.AspNet.ServerSentEvents
{
    /// <summary>
    /// Describes a store for Server Sent Events client data objects.
    /// </summary>
    /// <typeparam name="TCdo">Client data object type to store.</typeparam>
    public interface IServerSentEventsClientsDataStore<TCdo>
        where TCdo : ServerSentEventsClientData
    {
        /// <summary>
        /// Invoked when an event is being queued.
        /// </summary>
        public event EventQueueingEventHandler? EventQueueing;

        /// <summary>
        /// Invoked when events have been queued.
        /// </summary>
        public event EventsQueuedEventHandler? EventsQueued;

        /// <summary>
        /// Stores a client data object.
        /// </summary>
        Task StoreClientData(TCdo dataObject, HttpContext context, CancellationToken ct = default);

        /// <summary>
        /// Returns the client data object for the given identifer if found.
        /// </summary>
        bool TryGetClientData(Guid forIdentifier, [MaybeNullWhen(false)] out TCdo data);

        /// <summary>
        /// Queues a new event for the given client data object.
        /// </summary>
        public Task<bool> QueueNewEvent(ServerSentEvent @event, Guid identifier, CancellationToken ct = default);

        /// <summary>
        /// Queues a new event for client data objects matching the predicate.
        /// </summary>
        public Task QueueNewEvent(ServerSentEvent @event, Expression<Func<TCdo, bool>> predicate, CancellationToken ct = default);

        /// <summary>
        /// Clears client data for the given identifier.
        /// </summary>
        Task ClearClientData(Guid forIdentifier, CancellationToken ct = default);
        
        /// <summary>
        /// Enumerates Events for given identifier with id higher than given lastId.
        /// </summary>
        IEnumerable<ServerSentEvent> ListEvents(Guid forIdentifer, int lastId);
    }

    /// <summary>
    /// Arguments of the <see cref="IServerSentEventsClientsDataStore{TCdo}.EventQueueing"/> event.
    /// </summary>
    public class EventQueueingEventArgs : System.EventArgs
    {
        /// <summary>
        /// Constructs an <see cref="EventQueueingEventArgs"/> instance.
        /// </summary>
        public EventQueueingEventArgs(ServerSentEvent eventQueueing)
        {
            EventQueueing = eventQueueing;
        }

        /// <summary>
        /// The queueing event.
        /// </summary>
        public ServerSentEvent EventQueueing { get; private set; }
    }

    /// <summary>
    /// Delegate describing <see cref="IServerSentEventsClientsDataStore{TCdo}.EventQueueing"/> event handlers.
    /// </summary>
    public delegate void EventQueueingEventHandler(object sender, EventQueueingEventArgs e);

    /// <summary>
    /// Arguments of the <see cref="IServerSentEventsClientsDataStore{TCdo}.EventsQueued"/> event.
    /// </summary>
    public class EventsQueuedEventArgs : System.EventArgs
    {
        /// <summary>
        /// Constructs an <see cref="EventQueueingEventArgs"/> instance.
        /// </summary>
        public EventsQueuedEventArgs(IEnumerable<ServerSentEvent> eventsQueued)
        {
            EventsQueued = eventsQueued;
        }

        /// <summary>
        /// The queued events.
        /// </summary>
        public IEnumerable<ServerSentEvent> EventsQueued { get; private set; }
    }

    /// <summary>
    /// Delegate describing <see cref="IServerSentEventsClientsDataStore{TCdo}.EventsQueued"/> event handlers.
    /// </summary>
    public delegate void EventsQueuedEventHandler(object sender, EventsQueuedEventArgs e);
}
