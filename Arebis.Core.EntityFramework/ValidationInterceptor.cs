using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.EntityFramework
{
    /// <summary>
    /// An interceptor that validates entities befora saving.
    /// </summary>
    public class ValidationInterceptor : ISaveChangesInterceptor
    {
        /// <summary>
        /// An interceptor that validates entities before saving.
        /// </summary>
        /// <param name="validatePropertyValues">
        /// Whether to validate not only whether the property has a value (if [Required]),
        /// but also whether the value is valid towards other annotations as [Range] or [StringLength].
        /// True by default.
        /// </param>
        /// <param name="alsoValidateUnchanged">
        /// Whether to also validate unchanged entities. This can be required to validate rules based
        /// on the count of related entities. By default false: only modified and added entities are validated.
        /// </param>
        public ValidationInterceptor(bool validatePropertyValues = true, bool alsoValidateUnchanged = false)
        {
            ValidatePropertyValues = validatePropertyValues;
            AlsoValidateUnchanged = alsoValidateUnchanged;
        }

        /// <summary>
        /// Validate not only whether the property has a value (if [Required]),
        /// but also whether the value is valid towards other annotations as [Range] or [StringLength].
        /// </summary>
        public bool ValidatePropertyValues { get; }

        /// <summary>
        /// Validate not only modified and added entities, but also unchanged entities.
        /// Can be required to validate rules based on the count of related entities.
        /// </summary>
        public bool AlsoValidateUnchanged { get; }

        /// <inheritdoc/>
        public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context is not null)
            {
                ValidateEntitiesBeforeSaving(eventData.Context);
            }
            return result;
        }

        /// <inheritdoc/>
        public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                ValidateEntitiesBeforeSaving(eventData.Context);
            }

            return ValueTask.FromResult(result);
        }

        private void ValidateEntitiesBeforeSaving(DbContext context)
        {
            foreach (EntityEntry entry in context.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added || (this.AlsoValidateUnchanged && e.State == EntityState.Unchanged)).ToList())
            {
                var entity = entry.Entity;
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext, ValidatePropertyValues);
            }
        }
    }
}
