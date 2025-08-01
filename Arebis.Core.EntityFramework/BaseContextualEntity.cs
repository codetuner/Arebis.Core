using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.EntityFramework
{
    /// <summary>
    /// Abstract base entity class that implements <see cref="IContextualEntity{TContext}"/> to provide access to the context.
    /// </summary>
    public abstract class BaseContextualEntity<TContext> : IContextualEntity<TContext>
        where TContext : DbContext
    {
        /// <inheritdoc/>
        public TContext? Context { get; set; }
    }
}
