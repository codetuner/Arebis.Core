using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Arebis.Core.AspNet.ServerSentEvents
{
    /// <summary>
    /// A local memory Server Sent Events client data store suitable for single-process web applications
    /// not requiring persitence.
    /// </summary>
    /// <typeparam name="TCdo">Type of the client data.</typeparam>
    public class MemoryServerSentEventsClientsDataStore<TCdo> : IServerSentEventsClientsDataStore<TCdo>
        where TCdo : ServerSentEventsClientData
    {
        private ConcurrentDictionary<Guid, Tracker> store = [];
        private readonly IOptions<ServerSentEventsOptions> options;
        private readonly ILogger<MemoryServerSentEventsClientsDataStore<TCdo>> logger;

        /// <summary>
        /// Constructs a <see cref="MemoryServerSentEventsClientsDataStore{TCdo}"/> instance.
        /// </summary>
        public MemoryServerSentEventsClientsDataStore(IOptions<ServerSentEventsOptions> options, ILogger<MemoryServerSentEventsClientsDataStore<TCdo>> logger)
        {
            this.options = options;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public event EventQueueingEventHandler? EventQueueing;

        /// <inheritdoc/>
        public event EventsQueuedEventHandler? EventsQueued;

        /// <inheritdoc/>
        public Task StoreClientData(TCdo dataObject, HttpContext context, CancellationToken ct = default)
        {
            logger.LogInformation("Storing data for {identifier}.", dataObject.Identifier);

            // Check to apply retention:
            if (this.store.Count > (options.Value.RetentionCount + options.Value.RetentionOverrun)) ApplyRetention();

            // Store the client data object:
            store[dataObject.Identifier] = new Tracker(dataObject);

            // Return:
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public bool TryGetClientData(Guid forIdentifier, [MaybeNullWhen(false)] out TCdo data)
        {
            if (store.TryGetValue(forIdentifier, out var t))
            {
                logger.LogInformation("Data for {identifier} retrieved.", forIdentifier);
                data = t.DataObject;
                return true;
            }
            else
            {
                logger.LogInformation("Data for {identifier} not found.", forIdentifier);
                data = null!;
                return false;
            }
        }

        /// <inheritdoc/>
        public Task<bool> QueueNewEvent(ServerSentEvent @event, Guid identifier, CancellationToken ct = default)
        {
            if (store.TryGetValue(identifier, out var t))
            {
                @event = @event.Clone();
                @event.ClientIdentifier = identifier;
                QueueNewEventInternal(t, @event);
                OnEventsQueued(new EventsQueuedEventArgs(new ServerSentEvent[] { @event }));

                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false); 
            }
        }

        /// <inheritdoc/>
        public Task QueueNewEvent(ServerSentEvent @event, Expression<Func<TCdo, bool>> predicate, CancellationToken ct = default)
        {
            var cpredicate = predicate.Compile();
            var events = new List<ServerSentEvent>();
            foreach (var t in this.store.Values.Where(v => cpredicate(v.DataObject)))
            {
                var clone = @event.Clone();
                clone.ClientIdentifier = t.DataObject.Identifier;
                QueueNewEventInternal(t, clone);
                events.Add(clone);
            }
            OnEventsQueued(new EventsQueuedEventArgs(events));
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task QueueNewEvent(Func<ServerSentEvent> @eventFx, Expression<Func<TCdo, bool>> predicate, CancellationToken ct = default)
        {
            var cpredicate = predicate.Compile();
            var events = new List<ServerSentEvent>();
            var @event = (ServerSentEvent?)null;
            foreach (var t in this.store.Values.Where(v => cpredicate(v.DataObject)))
            {
                var clone = (@event == null) ? (@event = eventFx()) : @event.Clone();
                clone.ClientIdentifier = t.DataObject.Identifier;
                QueueNewEventInternal(t, clone);
                events.Add(clone);
            }
            OnEventsQueued(new EventsQueuedEventArgs(events));
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task QueueNewEvent(Func<TCdo, ServerSentEvent> @eventFx, Expression<Func<TCdo, bool>> predicate, CancellationToken ct = default)
        {
            var cpredicate = predicate.Compile();
            var events = new List<ServerSentEvent>();
            foreach (var t in this.store.Values.Where(v => cpredicate(v.DataObject)))
            {
                var clone = eventFx(t.DataObject);
                clone.ClientIdentifier = t.DataObject.Identifier;
                QueueNewEventInternal(t, clone);
                events.Add(clone);
            }
            OnEventsQueued(new EventsQueuedEventArgs(events));
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task ClearClientData(Guid forIdentifier, CancellationToken ct = default)
        {
            logger.LogInformation("Clear data for {identifier}.", forIdentifier);

            store.TryRemove(forIdentifier, out var client);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public IEnumerable<ServerSentEvent> ListEvents(Guid forIdentifer, int lastId)
        {
            if (store.TryGetValue(forIdentifer, out var t))
            {
                return t.DataObject.Events.Where(e => e.Id > lastId);
            }
            else
            { 
                return Enumerable.Empty<ServerSentEvent>();
            }
        }

        #region Internal implementation

        private IEnumerable<Tracker> QueryByPropertyInternal(string name, string? value)
        {
            foreach (var t in store.Values)
            {
                var property = typeof(TCdo).GetProperty(name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.IgnoreCase);
                if (property != null)
                {
                    if (Object.Equals(property.GetValue(t.DataObject), Convert.ChangeType(value, property.PropertyType)))
                    {
                        yield return t;
                    }
                }
            }
        }

        private void QueueNewEventInternal(Tracker t, ServerSentEvent @event)
        {
            @event.Id = ++t.DataObject.LastUsedId;
            t.LastUpdatedTicks = Environment.TickCount64;
            t.DataObject.Events.Add(@event);
            logger.LogDebug("Queue event {eventName} to {identifier.} with id {id}", @event.Type, @event.ClientIdentifier, t.DataObject.LastUsedId);
            OnEventQueueing(new EventQueueingEventArgs(@event));
        }

        private void OnEventQueueing(EventQueueingEventArgs args)
        {
            EventQueueing?.Invoke(this, args);
        }

        private void OnEventsQueued(EventsQueuedEventArgs args)
        {
            EventsQueued?.Invoke(this, args);
        }

        private void ApplyRetention()
        {
            var retentionTicks = DateTime.UtcNow.AddTicks(-options.Value.RetentionDuration.Ticks).Ticks;
            
            var retained = this.store.OrderByDescending(p => p.Value.LastUpdatedTicks)
                .Where(p => p.Value.LastUpdatedTicks > retentionTicks)
                .Take(options.Value.RetentionCount);

            this.store = new ConcurrentDictionary<Guid, Tracker>(retained);
        }

        private class Tracker
        {
            public Tracker(TCdo dataObject)
            {
                this.DataObject = dataObject;
                this.LastUpdatedTicks = Environment.TickCount64;
            }

            internal TCdo DataObject { get; }

            internal long LastUpdatedTicks { get; set; }
        }

        #endregion
    }
}
