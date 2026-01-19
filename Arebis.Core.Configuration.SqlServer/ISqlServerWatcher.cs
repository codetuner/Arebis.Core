using Microsoft.Extensions.Primitives;

namespace Arebis.Core.Configuration.SqlServer
{
    // Source: https://mousavi310.github.io/posts/a-refreshable-sql-server-configuration-provider-for-net-core/

    /// <summary>
    /// Defines a SQL Server watcher that provides change tokens for configuration changes.
    /// </summary>
    public interface ISqlServerWatcher : IDisposable
    {
        /// <summary>
        /// Watch method that returns an IChangeToken to monitor for configuration changes.
        /// </summary>
        IChangeToken Watch();
    }
}
