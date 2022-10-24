using Arebis.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Arebis.Core.EntityFramework.ValueConversion;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arebis.Core.EntityFramework
{
    /// <summary>
    /// A base class for DbContext with additional services.
    /// </summary>
    /// <typeparam name="TContext">The concrete DbContext type.</typeparam>
    public abstract class BaseDbContext<TContext> : DbContext
        where TContext : DbContext
    {
        #region Construction

        /// <inheritdoc/>
        public BaseDbContext(DbContextOptions options)
            : base(options)
        {
            this.ChangeTracker.Tracked += EntityTracked;
        }

        #endregion

        #region Model Creation

        /// <inheritdoc/>
        /// <remarks>
        /// When overriding, call base.OnModelCreating(modelBuilder) first.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Base impementation:
            base.OnModelCreating(modelBuilder);

            // Handle data annotations on context type:
            foreach (Attribute attribute in this.GetType().GetCustomAttributes(true).Cast<Attribute>())
            {
                // Handle DefaultSchemaAttribute:
                if (attribute is DefaultSchemaAttribute sattr)
                {
                    modelBuilder.HasDefaultSchema(sattr.SchemaName);
                }
            }

            // Handle data annotations on model types and properties:
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // On model types:
                foreach (Attribute attribute in entityType.ClrType.GetCustomAttributes(false).Cast<Attribute>())
                {
                    //...
                }

                // On model fields:
                foreach (var field in entityType.ClrType.GetFields(BindingFlags.NonPublic))
                {
                    foreach (Attribute attribute in field.GetCustomAttributes(false).Cast<Attribute>())
                    {
                        if (attribute is MappedFieldAttribute mfattr)
                        {
                            modelBuilder.Entity(entityType.ClrType).Property(field.Name);
                        }
                    }
                }

                // On model properties:
                var keyProperties = new List<PropertyInfo>();
                foreach (var property in entityType.ClrType.GetProperties())
                {
                    foreach (Attribute attribute in property.GetCustomAttributes(false).Cast<Attribute>())
                    {
                        // Handle KeyAttribute:
                        if (attribute is KeyAttribute kattr)
                        {
                            // Store the property. We'll only handle the case of composite keys not supported by EF Core:
                            keyProperties.Add(property);
                        }
                        // Handle ConverterAttribute:
                        if (attribute is ConverterAttribute cattr)
                        {
                            modelBuilder.Entity(entityType.ClrType).Property(property.Name)
                                .HasConversion((ValueConverter?)Activator.CreateInstance(cattr.ConverterType, cattr.ConstructorArgs));
                        }
                        // Handle TypeDiscriminatorAttribute:
                        else if (attribute is TypeDiscriminatorAttribute dattr)
                        {
                            modelBuilder.Entity(entityType.ClrType)
                                .HasDiscriminator(property.Name, property.PropertyType);
                        }
                    }
                }

                // Handle composite keys otherwise giving the exception:
                // System.InvalidOperationException: Entity type '...' has composite primary key defined with data annotations. To set composite primary key, use fluent API.
                if (keyProperties.Count > 1)
                {
                    var propertyNames = new string[keyProperties.Count];
                    foreach (var property in keyProperties)
                        propertyNames[property.GetCustomAttribute<ColumnAttribute>()?.Order ?? 0] = property.Name;
                    if (propertyNames.Any(n => n == null)) throw new InvalidOperationException($"Entity type '{entityType.Name}' has composite primary key with data annotations, but no Column annotation setting a unique zero-based order on each property.");
                    modelBuilder.Entity(entityType.ClrType).HasKey(propertyNames);
                }
            }
        }

        #endregion

        #region Contextual entity support and tracking

        private void EntityTracked(object? sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityTrackedEventArgs e)
        {
            if (e.Entry.Entity is IContextualEntity<TContext> entity)
            {
                if (e.Entry.Context is TContext context)
                {
                    entity.Context = context;
                }
            }

            this.OnEntityTracked(e.Entry);
        }

        /// <summary>
        /// Called when an entity is being tracked.
        /// </summary>
        public virtual void OnEntityTracked(EntityEntry entry)
        { }

        #endregion

        #region Saving

        /// <inheritdoc/>
        public override int SaveChanges()
        {
            return this.SaveChanges(true);
        }

        /// <inheritdoc/>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ContextSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <inheritdoc/>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return this.SaveChangesAsync(true, cancellationToken);
        }

        /// <inheritdoc/>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ContextSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ContextSaving()
        {
            // Handle individual changed/added/deleted entries:
            foreach (var entry in this.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Detached) continue;
                if (entry.State == EntityState.Unchanged) continue;
                if (entry.Entity is IInterceptingEntity interceptable)
                {
                    interceptable.OnSaving(entry);
                }
                this.OnEntitySaving(entry);
            }
        }

        /// <summary>
        /// Called before saving an added, changed or deleted entity.
        /// </summary>
        public virtual void OnEntitySaving(EntityEntry entry)
        { }

        #endregion
    }
}
