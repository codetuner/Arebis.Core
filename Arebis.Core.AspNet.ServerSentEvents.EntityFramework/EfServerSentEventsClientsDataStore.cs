using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Arebis.Core.AspNet.ServerSentEvents.EntityFramework
{
    /// <summary>
    /// A ServerSentEvents client data store build on EntityFramework.
    /// </summary>
    /// <typeparam name="TCdo">ClientData object type.</typeparam>
    public class EfServerSentEventsClientsDataStore<TCdo> : IServerSentEventsClientsDataStore<TCdo>
        where TCdo : ServerSentEventsClientData, new()
    {
        private readonly IServiceScopeFactory scopeFactory;

        /// <summary>
        /// Constructs a <see cref="EfServerSentEventsClientsDataStore{TCdo}"/>.
        /// </summary>
        public EfServerSentEventsClientsDataStore(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        /// <inheritdoc/>
        public event EventQueueingEventHandler? EventQueueing;

        /// <inheritdoc/>
        public event EventsQueuedEventHandler? EventsQueued;

        /// <inheritdoc/>
        public async Task ClearClientData(Guid forIdentifier, CancellationToken ct = default)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<EfServerSentEventsDbContext<TCdo>>();
                await dbContext.ClientDataEvents.Where(e => e.ClientIdentifier == forIdentifier).ExecuteDeleteAsync(ct);
                await dbContext.ClientData.Where(d => d.Identifier == forIdentifier).ExecuteDeleteAsync(ct);
            }
        }

        /// <inheritdoc/>
        public async Task StoreClientData(TCdo dataObject, HttpContext context, CancellationToken ct = default)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<EfServerSentEventsDbContext<TCdo>>();

                dbContext.ClientData.Add(dataObject);
                await dbContext.SaveChangesAsync(ct);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> QueueNewEvent(ServerSentEvent @event, Guid identifier, CancellationToken ct = default)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<EfServerSentEventsDbContext<TCdo>>();

                var dataObject = dbContext.ClientData.Find(identifier);
                if (dataObject != null)
                {
                    var entity = @event.Clone();
                    var entry = dbContext.ClientDataEvents.Add(entity);
                    entity.Id = ++dataObject.LastUsedId;
                    entity.ClientIdentifier = identifier;
                    dbContext.Entry(dataObject).Property("LastEventQueuedTime").CurrentValue = DateTime.UtcNow;

                    OnEventQueueing(new EventQueueingEventArgs(entity));

                    await dbContext.SaveChangesAsync(ct);

                    OnEventsQueued(new EventsQueuedEventArgs(new ServerSentEvent[] { entity }));

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <inheritdoc/>
        public async Task QueueNewEvent(ServerSentEvent @event, Expression<Func<TCdo, bool>> predicate, CancellationToken ct = default)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<EfServerSentEventsDbContext<TCdo>>();

                var events = new List<ServerSentEvent>();
                foreach(var dataObject in await dbContext.ClientData.Where(predicate).ToListAsync(ct))
                {
                    var entity = @event.Clone();
                    var entry = dbContext.ClientDataEvents.Add(entity);
                    entity.Id = ++dataObject.LastUsedId;
                    entity.ClientIdentifier = dataObject.Identifier;
                    dbContext.Entry(dataObject).Property("LastEventQueuedTime").CurrentValue = DateTime.UtcNow;

                    OnEventQueueing(new EventQueueingEventArgs(entity));

                    events.Add(entity);
                }

                await dbContext.SaveChangesAsync(ct);

                OnEventsQueued(new EventsQueuedEventArgs(events));
            }
        }

        /// <inheritdoc/>
        public bool TryGetClientData(Guid forIdentifier, [MaybeNullWhen(false)] out TCdo data)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<EfServerSentEventsDbContext<TCdo>>();
                var entity = dbContext.ClientData.AsNoTracking()
                    .Include(d => d.Events)
                    .SingleOrDefault(d => d.Identifier == forIdentifier);
                if (entity != null)
                {
                    data = entity;
                    return true;
                }
                else
                {
                    data = null;
                    return false;
                }
            }
        }

        /// <inheritdoc/>
        public IEnumerable<ServerSentEvent> ListEvents(Guid forIdentifer, int lastId)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<EfServerSentEventsDbContext<TCdo>>();

                return dbContext.ClientData.AsNoTracking()
                    .Where(d => d.Identifier == forIdentifer)
                    .SelectMany(d => d.Events)
                    .Where(e => e.Id > lastId)
                    .ToList();                
            }
        }

        #region Internal implementation

        private void OnEventQueueing(EventQueueingEventArgs args)
        {
            EventQueueing?.Invoke(this, args);
        }

        private void OnEventsQueued(EventsQueuedEventArgs args)
        {
            EventsQueued?.Invoke(this, args);
        }

        #endregion
    }
}
