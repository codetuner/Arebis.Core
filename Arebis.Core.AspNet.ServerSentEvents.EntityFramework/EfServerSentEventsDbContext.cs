using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Arebis.Core.AspNet.ServerSentEvents.EntityFramework
{
    /// <summary>
    /// A DbContext implementation for the <see cref="EfServerSentEventsClientsDataStore{TCdo}"/>.
    /// </summary>
    /// <typeparam name="TCdo">ClientData object type.</typeparam>
    public class EfServerSentEventsDbContext<TCdo> : DbContext
        where TCdo : ServerSentEventsClientData
    {
        private readonly IOptions<ServerSentEventsOptions> sseOptions;

        /// <summary>
        /// Constructs an <see cref="EfServerSentEventsDbContext{TCdo}"/> instance.
        /// </summary>
        public EfServerSentEventsDbContext(DbContextOptions<EfServerSentEventsDbContext<TCdo>> options, IOptions<ServerSentEventsOptions> sseOptions)
            : base(options)
        {
            this.sseOptions = sseOptions;
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(sseOptions.Value.DatabaseSchema);

            modelBuilder.Entity<TCdo>()
                .HasKey(e => e.Identifier);

            modelBuilder.Entity<TCdo>()
                .Property<DateTime?>("LastEventQueuedTime");

            modelBuilder.Entity<ServerSentEvent>()
                .HasKey("ClientIdentifier", "Id");

            modelBuilder.Entity<TCdo>()
                .HasMany(e => e.Events)
                .WithOne()
                .HasForeignKey("ClientIdentifier")
                .IsRequired();
        }

        /// <summary>
        /// Set of client data entities.
        /// </summary>
        public DbSet<TCdo> ClientData { get; set; } = null!;

        /// <summary>
        /// Set of client data event entities.
        /// </summary>
        public DbSet<ServerSentEvent> ClientDataEvents { get; set; } = null!;
    }
}
