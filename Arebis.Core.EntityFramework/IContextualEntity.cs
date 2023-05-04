using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.Core.EntityFramework
{
    /// <summary>
    /// Interface implemented by context-aware entities.
    /// Context-aware entities are entities that know their context.
    /// </summary>
    /// <typeparam name="TContext">Type of the context.</typeparam>
    public interface IContextualEntity<TContext>
        where TContext : DbContext
    {
        /// <summary>
        /// Context of the entity.
        /// </summary>
        TContext? Context { get; set; }
    }
}
