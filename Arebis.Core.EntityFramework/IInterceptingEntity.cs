using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.Core.EntityFramework
{
    /// <summary>
    /// An entity that can intercept its own save operation.
    /// </summary>
    public interface IInterceptingEntity
    {
        /// <summary>
        /// Called when saving this entity.
        /// </summary>
        void OnSaving(EntityEntry entityEntry);
    }
}
