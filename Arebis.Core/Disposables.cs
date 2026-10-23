using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Owns disposable resources and disposes them in reverse order of registration.
    /// Supports both <see cref="IDisposable"/> and <see cref="IAsyncDisposable"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Resources are disposed in LIFO order.
    /// </para>
    /// <para>
    /// When <see cref="DisposeAsync"/> is used, asynchronous disposal is preferred
    /// for resources implementing <see cref="IAsyncDisposable"/>.
    /// </para>
    /// <para>
    /// When <see cref="Dispose"/> is used, all resources must implement
    /// <see cref="IDisposable"/>. If an async-only resource has been registered,
    /// <see cref="Dispose"/> throws without disposing any resources.
    /// </para>
    /// <para>
    /// This type is not thread-safe.
    /// </para>
    /// </remarks>
    public sealed class Disposables : IDisposable, IAsyncDisposable
    {
        private readonly Stack<Entry> disposables = new();

        private bool disposed;
        private bool containsAsyncOnlyDisposable;

        /// <summary>
        /// Adds a synchronously disposable resource.
        /// </summary>
        /// <typeparam name="T">The resource type.</typeparam>
        /// <param name="disposable">The resource to own.</param>
        /// <returns>The supplied resource.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="disposable"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// This instance has already been disposed.
        /// </exception>
        public T Add<T>(T disposable)
            where T : IDisposable
        {
            ArgumentNullException.ThrowIfNull(disposable);
            ThrowIfDisposed();

            disposables.Push(new Entry(disposable));

            return disposable;
        }

        /// <summary>
        /// Adds an asynchronously disposable resource.
        /// </summary>
        /// <typeparam name="T">The resource type.</typeparam>
        /// <param name="disposable">The resource to own.</param>
        /// <returns>The supplied resource.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="disposable"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        /// This instance has already been disposed.
        /// </exception>
        public T AddAsync<T>(T disposable)
            where T : IAsyncDisposable
        {
            ArgumentNullException.ThrowIfNull(disposable);
            ThrowIfDisposed();

            var synchronousDisposable = disposable as IDisposable;

            disposables.Push(
                new Entry(
                    synchronousDisposable,
                    disposable));

            if (synchronousDisposable is null)
            {
                containsAsyncOnlyDisposable = true;
            }

            return disposable;
        }

        /// <summary>
        /// Disposes all registered resources synchronously in reverse order
        /// of registration.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// At least one registered resource supports only asynchronous disposal.
        /// Use <see cref="DisposeAsync"/> instead.
        /// </exception>
        /// <exception cref="AggregateException">
        /// One or more resources threw while being disposed.
        /// </exception>
        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            // Important: validate before disposing anything.
            //
            // Otherwise we could partially dispose the collection and then
            // discover an async-only resource halfway through.
            if (containsAsyncOnlyDisposable)
            {
                throw new InvalidOperationException(
                    $"This {nameof(Disposables)} contains one or more resources " +
                    $"that support only {nameof(IAsyncDisposable)}. " +
                    $"Use {nameof(DisposeAsync)}() instead.");
            }

            disposed = true;

            List<Exception>? exceptions = null;

            while (disposables.TryPop(out var entry))
            {
                try
                {
                    entry.Disposable!.Dispose();
                }
                catch (Exception ex)
                {
                    (exceptions ??= new List<Exception>()).Add(ex);
                }
            }

            if (exceptions is not null)
            {
                throw new AggregateException(
                    "One or more errors occurred while disposing resources.",
                    exceptions);
            }
        }

        /// <summary>
        /// Disposes all registered resources in reverse order of registration,
        /// using asynchronous disposal when available.
        /// </summary>
        /// <exception cref="AggregateException">
        /// One or more resources threw while being disposed.
        /// </exception>
        public async ValueTask DisposeAsync()
        {
            if (disposed)
            {
                return;
            }

            disposed = true;

            List<Exception>? exceptions = null;

            while (disposables.TryPop(out var entry))
            {
                try
                {
                    if (entry.AsyncDisposable is not null)
                    {
                        await entry.AsyncDisposable
                            .DisposeAsync()
                            .ConfigureAwait(false);
                    }
                    else
                    {
                        entry.Disposable!.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    (exceptions ??= new List<Exception>()).Add(ex);
                }
            }

            if (exceptions is not null)
            {
                throw new AggregateException(
                    "One or more errors occurred while disposing resources.",
                    exceptions);
            }
        }

        private void ThrowIfDisposed()
        {
            ObjectDisposedException.ThrowIf(disposed, this);
        }

        private readonly struct Entry
        {
            public Entry(IDisposable disposable)
            {
                Disposable = disposable;

                // If the object happens to implement both interfaces,
                // DisposeAsync() should prefer asynchronous disposal.
                AsyncDisposable = disposable as IAsyncDisposable;
            }

            public Entry(
                IDisposable? disposable,
                IAsyncDisposable asyncDisposable)
            {
                Disposable = disposable;
                AsyncDisposable = asyncDisposable;
            }

            public IDisposable? Disposable { get; }

            public IAsyncDisposable? AsyncDisposable { get; }
        }
    }
}
