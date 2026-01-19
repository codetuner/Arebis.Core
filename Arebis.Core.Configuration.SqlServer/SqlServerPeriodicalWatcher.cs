using Microsoft.Extensions.Primitives;

namespace Arebis.Core.Configuration.SqlServer
{
    // Source: https://mousavi310.github.io/posts/a-refreshable-sql-server-configuration-provider-for-net-core/

    internal class SqlServerPeriodicalWatcher : ISqlServerWatcher
    {
        private readonly TimeSpan refreshInterval;
        private IChangeToken? changeToken;
        private readonly Timer timer;
        private CancellationTokenSource? cancellationTokenSource;

        public SqlServerPeriodicalWatcher(TimeSpan refreshInterval)
        {
            this.refreshInterval = refreshInterval;
            timer = new Timer(Change, null, TimeSpan.Zero, this.refreshInterval);
        }

        private void Change(object? state)
        {
            cancellationTokenSource?.Cancel();
        }

        public IChangeToken Watch()
        {
            cancellationTokenSource = new CancellationTokenSource();
            changeToken = new CancellationChangeToken(cancellationTokenSource.Token);

            return changeToken;
        }

        public void Dispose()
        {
            timer?.Dispose();
            cancellationTokenSource?.Dispose();
        }
    }
}
