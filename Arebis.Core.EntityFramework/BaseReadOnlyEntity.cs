using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Arebis.Core.EntityFramework
{
    /// <summary>
    /// A base class for read-only entities that are associated with a specific DbContext.
    /// Prevents saving changes to the database.
    /// </summary>
    public abstract class BaseReadOnlyEntity : IInterceptingEntity
    {
        /// <inheritdoc />
        public bool OnSaving(EntityEntry entityEntry)
        {
            if (entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified || entityEntry.State == EntityState.Deleted)
            {
                throw new InvalidOperationException("Read-only entities cannot be saved to the database.");
            }
            else
            {
                return true; // Allow other operations like querying
            }
        }
    }

    /// <summary>
    /// A base class for read-only entities that are associated with a specific DbContext.
    /// Prevents saving changes to the database.
    /// Implements <see cref="IContextualEntity{TContext}"/> to provide access to the context.
    /// </summary>
    /// <typeparam name="TContext">The context type the entity type belongs to.</typeparam>
    public abstract class BaseReadOnlyEntity<TContext> : BaseReadOnlyEntity, IContextualEntity<TContext>
        where TContext : DbContext
    {
        /// <inheritdoc />
        public TContext? Context { get; set; }
    }
}
