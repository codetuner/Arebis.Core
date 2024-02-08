using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.EntityFramework
{
    /// <summary>
    /// EntityFramework interceptor to support [StoreEmptyAsNull] attributes.
    /// </summary>
    public class StoreEmptyAsNullInterceptor : ISaveChangesInterceptor, IMaterializationInterceptor
    {
        private static ConcurrentDictionary<Type, PropertyInfo[]> metaCache = new ConcurrentDictionary<Type, PropertyInfo[]>();

        /// <inheritdoc/>
        public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is not null)
            {
                UpdateEntitiesBeforeSaving(eventData.Context);
            }
            return result;
        }

        /// <inheritdoc/>
        public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                UpdateEntitiesBeforeSaving(eventData.Context);
            }

            return ValueTask.FromResult(result);
        }

        private void UpdateEntitiesBeforeSaving(DbContext context)
        {
            foreach (EntityEntry entry in context.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added))
            {
                var entity = entry.Entity;
                foreach (var property in GetNullOnEmptyPropertiesFor(entity.GetType()))
                {
                    //var currentValue = (string?)property.GetValue(entity);
                    var currentValue = (string?)entry.Property(property.Name).CurrentValue;
                    if (currentValue != null && String.IsNullOrWhiteSpace(currentValue))
                    {
                        entry.Property(property.Name).CurrentValue = null;
                    }
                }
            }
        }

        /// <inheritdoc/>
        public object InitializedInstance(MaterializationInterceptionData materializationData, object entity)
        {
            foreach (var property in GetNullOnEmptyPropertiesFor(entity.GetType()))
            {
                if (property.GetValue(entity) == null) property.SetValue(entity, String.Empty);
            }

            return entity;
        }

        private PropertyInfo[] GetNullOnEmptyPropertiesFor(Type type)
        {
            if (metaCache.TryGetValue(type, out var properties))
            {
                return properties;
            }
            else
            {
                var propertiesList = new List<PropertyInfo>();
                foreach (var property in type.GetProperties())
                {
                    if (property.GetCustomAttributes(typeof(StoreEmptyAsNullAttribute), true).Any())
                    {
                        propertiesList.Add(property);
                    }
                }
                if (propertiesList.Count > 0)
                {
                    return metaCache[type] = propertiesList.ToArray();
                }
                else
                {
                    return metaCache[type] = Array.Empty<PropertyInfo>();
                }
            }
        }
    }
}
