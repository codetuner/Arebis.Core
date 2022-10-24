using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.Core.EntityFramework
{
    /// <summary>
    /// Default relational schema for this DbContext.
    /// Requires the DbContext to inherit from BaseDbContext.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DefaultSchemaAttribute : Attribute
    {
        /// <summary>
        /// Sets the default relational schema for this DbContext.
        /// </summary>
        public DefaultSchemaAttribute(string schemaName)
        {
            this.SchemaName = schemaName;
        }

        /// <summary>
        /// The default relational schema of this DbContext.
        /// </summary>
        public string SchemaName { get; set; }
    }
}
