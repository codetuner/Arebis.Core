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
using System.Collections;

namespace Arebis.Core.EntityFramework
{
    /// <summary>
    /// EntityFramework interceptor to support [StoreEmptyAsNull] attributes.
    /// </summary>
    public class StoreEmptyAsNullInterceptor : ISaveChangesInterceptor, IMaterializationInterceptor
    {
        private static ConcurrentDictionary<Type, Tuple<StoreEmptyAsNullAttribute, PropertyInfo>[]> metaCache = new ConcurrentDictionary<Type, Tuple<StoreEmptyAsNullAttribute, PropertyInfo>[]>();

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
                    if (property.Item2.PropertyType == typeof(string))
                    {
                        //var currentValue = (string?)property.GetValue(entity);
                        var currentValue = (string?)entry.Property(property.Item2.Name).CurrentValue;
                        if (currentValue != null && String.IsNullOrWhiteSpace(currentValue))
                        {
                            entry.Property(property.Item2.Name).CurrentValue = null;
                        }
                    }
                    else
                    {
                        var currentValue = (IEnumerable?)entry.Property(property.Item2.Name).CurrentValue;
                        if (currentValue != null && IsEnumerableEmpty(currentValue))
                        {
                            entry.Property(property.Item2.Name).CurrentValue = null;
                        }
                    }
                }
            }
        }

        /// <inheritdoc/>
        public object InitializedInstance(MaterializationInterceptionData materializationData, object entity)
        {
            foreach (var property in GetNullOnEmptyPropertiesFor(entity.GetType()))
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

        private Tuple<StoreEmptyAsNullAttribute, PropertyInfo>[] GetNullOnEmptyPropertiesFor(Type type)
        {
            if (metaCache.TryGetValue(type, out var properties))
            {
                return properties;
            }
            else
            {
                var propertiesList = new List<Tuple<StoreEmptyAsNullAttribute, PropertyInfo>>();
                foreach (var property in type.GetProperties())
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

        private static bool IsEnumerableEmpty(IEnumerable e)
        {
            foreach (var item in e) return false;
            return true;
        }
    }
}
