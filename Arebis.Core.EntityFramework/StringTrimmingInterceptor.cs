using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.EntityFramework
{
    /// <summary>
    /// Interceptor that trimms changed string values before storing them.
    /// </summary>
    public class StringTrimmingInterceptor : ISaveChangesInterceptor
    {
        /// <summary>
        /// Interceptor that trimms changed string values before storing them.
        /// </summary>
        public StringTrimmingInterceptor(bool storeEmptyAsNull = true)
        {
            StoreEmptyAsNull = storeEmptyAsNull;
        }

        /// <summary>
        /// Whether to store empty strings as null if the property is nullable.
        /// </summary>
        public bool StoreEmptyAsNull { get; }

        /// <inheritdoc/>
        public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is not null)
            {
                TrimStringsBeforeSaving(eventData.Context);
            }
            return result;
        }

        /// <inheritdoc/>
        public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                TrimStringsBeforeSaving(eventData.Context);
            }

            return ValueTask.FromResult(result);
        }

        private void TrimStringsBeforeSaving(DbContext context)
        {
            foreach (EntityEntry entry in context.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added))
            {
                foreach (var property in entry.Properties.Where(p => p.CurrentValue is string && (p.IsModified || entry.State == EntityState.Added)))
                {
                    var current = (string)property.CurrentValue!;
                    var trimmed = current.Trim();
                    if (trimmed.Length == 0 && StoreEmptyAsNull && property.Metadata.IsNullable)
                    {
                        property.CurrentValue = null;
                    }
                    else if (current.Length != trimmed.Length)
                    {
                        property.CurrentValue = trimmed;
                    }
                }
            }
        }
    }
}
