using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.EntityFramework
{
    /// <summary>
    /// EntityFramework interceptor to support [StoreEmptyAsNull] attributes.
    /// </summary>
    public class StoreEmptyAsNullInterceptor : ISaveChangesInterceptor, IMaterializationInterceptor
    {
        // Automatically allows GC when DbContext is collected.
        private static readonly ConditionalWeakTable<DbContext, List<(object Entity, PropertyInfo Property, object? OriginalValue)>> pending = new();

        private static ConcurrentDictionary<Type, Tuple<StoreEmptyAsNullAttribute, PropertyInfo>[]> metaCache = new ConcurrentDictionary<Type, Tuple<StoreEmptyAsNullAttribute, PropertyInfo>[]>();

        /// <summary>
        /// Before saving changes, convert empty strings or collections to nulls.
        /// </summary>
        public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is not null)
            {
                UpdateEntitiesBeforeSaving(eventData.Context);
            }

            return result;
        }

        /// <summary>
        /// Before saving changes, convert empty strings or collections to nulls.
        /// </summary>
        public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                UpdateEntitiesBeforeSaving(eventData.Context);
            }

            return ValueTask.FromResult(result);
        }

        /// <summary>
        /// Sets entity properties to null where applicable before saving.
        /// </summary>
        private void UpdateEntitiesBeforeSaving(DbContext context)
        {
            var changesToRestore = new List<(object Entity, PropertyInfo Property, object? OriginalValue)>();

            foreach (EntityEntry entry in context.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added))
            {
                var entity = entry.Entity;
                foreach (var property in GetNullOnEmptyPropertiesFor(entity.GetType(), context))
                {
                    var entryProperty = entry.Property(property.Item2.Name);
                    if (property.Item2.PropertyType == typeof(string))
                    {
                        //var currentValue = (string?)property.GetValue(entity);
                        var currentValue = (string?)entryProperty.CurrentValue;
                        if (currentValue != null && String.IsNullOrWhiteSpace(currentValue))
                        {
                            changesToRestore.Add((entity, property.Item2, currentValue));
                            entryProperty.CurrentValue = null;
                        }
                    }
                    else
                    {
                        var currentValue = entryProperty.CurrentValue as IEnumerable;
                        if (currentValue != null && IsEnumerableEmpty(currentValue))
                        {
                            changesToRestore.Add((entity, property.Item2, currentValue));
                            entryProperty.CurrentValue = null;
                        }
                    }
                }
            }

            // Replace any previous captured set (nested SaveChanges is rare but possible).
            pending.Remove(context);
            pending.Add(context, changesToRestore);
        }

        /// <summary>
        /// After saving changes, restore empty strings or collections if applicable.
        /// </summary>
        public int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            if (eventData.Context is not null)
            {
                UpdateEntitiesAfterSaving(eventData.Context);
            }

            return result;
        }

        /// <summary>
        /// After saving changes, restore empty strings or collections if applicable.
        /// </summary>
        public ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                UpdateEntitiesAfterSaving(eventData.Context);
            }

            return ValueTask.FromResult(result);
        }

        /// <summary>
        /// Sets entity properties back to their original (non-null empty) values after saving.
        /// </summary>
        private void UpdateEntitiesAfterSaving(DbContext context)
        {
            if (pending.TryGetValue(context, out var changesToRestore))
            { 
                foreach (var change in changesToRestore)
                {
                    change.Property.SetValue(change.Entity, change.OriginalValue);
                }
                pending.Remove(context);
            }
        }

        /// <summary>
        /// On materializing an entity, convert nulls to empty strings or collections.
        /// </summary>
        public object InitializedInstance(MaterializationInterceptionData materializationData, object entity)
        {
            foreach (var property in GetNullOnEmptyPropertiesFor(entity.GetType(), materializationData.Context))
            {
                if (property.Item2.GetValue(entity) == null)
                {
                    var value = (property.Item2.PropertyType == typeof(string))
                        ? String.Empty
                        : Activator.CreateInstance(property.Item1.InstanceType ?? property.Item2.PropertyType);
                    property.Item2.SetValue(entity, value);
                }
            }

            return entity;
        }

        /// <summary>
        /// Return attribute and property info of all properties with [StoreEmptyAsNull] attribute of the given type.
        /// </summary>
        private Tuple<StoreEmptyAsNullAttribute, PropertyInfo>[] GetNullOnEmptyPropertiesFor(Type type, DbContext context)
        {
            if (metaCache.TryGetValue(type, out var properties))
            {
                return properties;
            }
            else
            {
                var propertiesList = new List<Tuple<StoreEmptyAsNullAttribute, PropertyInfo>>();
                var clrtype = context.Model.FindRuntimeEntityType(type)?.ClrType ?? type;
                foreach (var property in clrtype.GetProperties())
                {
                    var attributes = property.GetCustomAttributes(typeof(StoreEmptyAsNullAttribute), true);
                    if (attributes.Any())
                    {
                        var attribute = (StoreEmptyAsNullAttribute)attributes[0];
                        propertiesList.Add( new Tuple<StoreEmptyAsNullAttribute, PropertyInfo>(attribute, property));
                    }
                }
                if (propertiesList.Count > 0)
                {
                    return metaCache[type] = propertiesList.ToArray();
                }
                else
                {
                    return metaCache[type] = Array.Empty<Tuple<StoreEmptyAsNullAttribute, PropertyInfo>>();
                }
            }
        }

        /// <summary>
        /// Tests whether the given enumerable is empty.
        /// </summary>
        private static bool IsEnumerableEmpty(IEnumerable e)
        {
            foreach (var item in e) return false;
            return true;
        }
    }
}
