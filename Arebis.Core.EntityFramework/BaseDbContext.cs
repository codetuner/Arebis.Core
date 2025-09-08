using Arebis.Core.EntityFramework.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;

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

            // Configure default converters:
            var defaultConverters = new Dictionary<Type, ConverterAttribute?>();
            defaultConverters.Add(typeof(System.Boolean), null);
            defaultConverters.Add(typeof(System.Byte), null);
            defaultConverters.Add(typeof(System.Int16), null);
            defaultConverters.Add(typeof(System.Int32), null);
            defaultConverters.Add(typeof(System.Int64), null);
            defaultConverters.Add(typeof(System.Decimal), null);
            defaultConverters.Add(typeof(System.String), null);
            defaultConverters.Add(typeof(System.DateTime), null);
            defaultConverters.Add(typeof(System.DateOnly), null);
            defaultConverters.Add(typeof(System.TimeOnly), null);
            defaultConverters.Add(typeof(System.TimeSpan), null);
            ConfigureDefaultConverters(defaultConverters);

            // Handle data annotations on context type:
            foreach (Attribute attribute in this.GetType().GetCustomAttributes(true).Cast<Attribute>())
            {
                // Handle DefaultSchemaAttribute:
                if (attribute is DefaultSchemaAttribute sattr)
                {
                    modelBuilder.HasDefaultSchema(sattr.SchemaName);
                }
            }

            // Handle data annotations on model types which can result in additional entity types to be discovered:
            foreach (var entityType in modelBuilder.Model.GetEntityTypes().ToList())
            {
                // On model types:
                foreach (Attribute attribute in entityType.ClrType.GetCustomAttributes(false).Cast<Attribute>())
                {
                    // The [TypeDiscriminatorValue] attributes can cause new entity types to be discovered
                    // which is why this attribute must be handled in a loop before handling other attributes:
                    if (attribute is TypeDiscriminatorAttribute discrattr)
                    {
                        var discbuilder = modelBuilder.Entity(entityType.ClrType)
                            .HasDiscriminator(discrattr.PropertyName, discrattr.PropertyType);

                        if (!entityType.ClrType.IsAbstract)
                        {
                            var discval = entityType.ClrType.GetCustomAttribute<TypeDiscriminatorValueAttribute>();
                            if (discval != null)
                            {
                                discbuilder.HasValue(entityType.ClrType, discval.Value);
                            }
                        }
                        foreach (var subType in entityType.ClrType.Assembly.GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(entityType.ClrType)))
                        {
                            var discval = subType.GetCustomAttribute<TypeDiscriminatorValueAttribute>();
                            if (discval != null)
                            {
                                discbuilder.HasValue(subType, discval.Value);
                            }
                        }
                    }
                }
            }
            
            // Handle data annotations on model types and properties:
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // On model types:
                foreach (Attribute attribute in entityType.ClrType.GetCustomAttributes(false).Cast<Attribute>())
                {
                    // ...
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
                foreach (var property in entityType.ClrType.GetProperties())
                {
                    var notMapped = false;
                    var hasConversion = false;
                    foreach (Attribute attribute in property.GetCustomAttributes(false).Cast<Attribute>())
                    {
                        // Handle ConverterAttribute:
                        if (attribute is ConverterAttribute cattr)
                        {
                            modelBuilder.Entity(entityType.ClrType).Property(property.Name)
                                .HasConversion(
                                    (ValueConverter?)Activator.CreateInstance(cattr.ConverterType, cattr.ConverterConstructorArgs),
                                    (cattr.ComparerType != null) ? (ValueComparer?)Activator.CreateInstance(cattr.ComparerType, cattr.ComparerConstructorArgs) : null
                                );
                            hasConversion = true;
                        }
                        // Handle TypeDiscriminatorAttribute:
                        else if (attribute is TypeDiscriminatorAttribute dattr)
                        {
                            modelBuilder.Entity(entityType.ClrType)
                                .HasDiscriminator(property.Name, property.PropertyType);
                        }
                        // Handle StoreEmptyAsNullAttribute:
                        else if (attribute is StoreEmptyAsNullAttribute easnattr)
                        {
                            // Mark property as not required in the database as it must be able to store NULL values:
                            modelBuilder.Entity(entityType.ClrType)
                                .Property(property.Name)
                                .IsRequired(false);
                        }
                        // Handle NotMapped:
                        else if (attribute is NotMappedAttribute)
                        {
                            notMapped = true;
                        }
                    }
                    // Check for a Converter attribute defined as default:
                    var propertyTypeNotNullable = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                    if (!notMapped && !hasConversion && defaultConverters.TryGetValue(propertyTypeNotNullable, out ConverterAttribute? cattr2))
                    {
                        if (cattr2 != null)
                        {
                            modelBuilder.Entity(entityType.ClrType).Property(property.Name)
                                .HasConversion(
                                    (ValueConverter?)Activator.CreateInstance(cattr2.ConverterType, cattr2.ConverterConstructorArgs),
                                    (cattr2.ComparerType != null) ? (ValueComparer?)Activator.CreateInstance(cattr2.ComparerType, cattr2.ComparerConstructorArgs) : null
                                );
                        }
                    }
                    // Or check for Converter attribute defined on property's PropertyType:
                    else if (!notMapped && !hasConversion)
                    {
                        var cattr3 = (ConverterAttribute?)propertyTypeNotNullable.GetCustomAttribute(typeof(ConverterAttribute));
                        defaultConverters.Add(propertyTypeNotNullable, cattr3);
                        if (cattr3 != null)
                        {
                            modelBuilder.Entity(entityType.ClrType).Property(property.Name)
                                .HasConversion(
                                    (ValueConverter?)Activator.CreateInstance(cattr3.ConverterType, cattr3.ConverterConstructorArgs),
                                    (cattr3.ComparerType != null) ? (ValueComparer?)Activator.CreateInstance(cattr3.ComparerType, cattr3.ComparerConstructorArgs) : null
                                );
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Override this method to configure default converter attributes for property types.
        /// </summary>
        protected virtual void ConfigureDefaultConverters(IDictionary<Type, ConverterAttribute?> converters)
        { }

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

        private List<Action> afterSaveActions = new List<Action>();

        /// <summary>
        /// Registers an action to be executed after the next successful SaveChanges() call.
        /// </summary>
        public void AddAfterSaveAction(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            this.afterSaveActions.Add(action);
        }

        /// <inheritdoc/>
        public override int SaveChanges()
        {
            return this.SaveChanges(true);
        }

        /// <inheritdoc/>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            // Perform pre-saving operations:
            ContextSaving();

            // Then effectively save changes;
            // ContextSaving already triggered change-detection, therefore avoid to redo change-detection:
            try
            {
                ChangeTracker.AutoDetectChangesEnabled = false;

                // Execute the base SaveChanges:
                var result = base.SaveChanges(acceptAllChangesOnSuccess);

                // Execute after-save actions:
                this.afterSaveActions.ForEach(action => action());
                this.afterSaveActions.Clear();

                // Return the result:
                return result;
            }
            finally
            {
                ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }

        /// <inheritdoc/>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await this.SaveChangesAsync(true, cancellationToken);
        }

        /// <inheritdoc/>
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            // Perform pre-saving operations:
            ContextSaving();

            // Then effectively save changes;
            // ContextSaving already triggered change-detection, therefore avoid to redo change-detection:
            try
            {
                ChangeTracker.AutoDetectChangesEnabled = false;

                // Execute the base SaveChanges:
                var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

                // Execute after-save actions:
                this.afterSaveActions.ForEach(action => action());
                this.afterSaveActions.Clear();

                // Return the result:
                return result;
            }
            finally
            {
                ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }

        private void ContextSaving()
        {
            // Handle individual changed/added/deleted entries:
            // (ChangeTracker.Entries() also triggers change-detection)
            var mayHaveChanges = false;
            foreach (var entry in this.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Detached) continue;
                if (entry.State == EntityState.Unchanged) continue;
                if (entry.Entity is IInterceptingEntity interceptable)
                {
                    mayHaveChanges = mayHaveChanges || interceptable.OnSaving(entry);
                }
                mayHaveChanges = mayHaveChanges || this.OnEntitySaving(entry);

                // If changes may have occured, redo detect changes:
                if (mayHaveChanges)
                {
                    this.ChangeTracker.DetectChanges();
                }
            }
        }

        /// <summary>
        /// Called before saving an added, changed or deleted entity.
        /// </summary>
        /// <returns>
        /// True if the method may have performed changes on entities, false otherwise.
        /// </returns>
        public virtual bool OnEntitySaving(EntityEntry entry)
        {
            return false;
        }

        #endregion
    }
}
