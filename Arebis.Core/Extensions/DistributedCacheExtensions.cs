using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Extensions
{
    /// <summary>
    /// DistributedCache extension methods.
    /// </summary>
    public static class DistributedCacheExtensions
    {
        /// <summary>
        /// Gets an item from the distributed cache.
        /// The item is deserialized from JSON format.
        /// </summary>
        public static async Task<T?> GetAsync<T>(this IDistributedCache cache, string key, CancellationToken ct = default)
        {
            var data = await cache.GetStringAsync(key, ct);
            return data is null ? default : System.Text.Json.JsonSerializer.Deserialize<T>(data);
        }

        /// <summary>
        /// Writes an item to the distributed cache.
        /// The item is serialized in JSON format.
        /// </summary>
        public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, CancellationToken ct = default)
        {
            var options = new DistributedCacheEntryOptions();
            await cache.SetAsync(key, value, options, ct);
        }

        /// <summary>
        /// Writes an item to the distributed cache.
        /// The item is serialized in JSON format.
        /// </summary>
        public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, TimeSpan? absoluteExpirationRelativeToNow, CancellationToken ct = default)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow,
            };
            await cache.SetAsync(key, value, options, ct);
        }

        /// <summary>
        /// Writes an item to the distributed cache.
        /// The item is serialized in JSON format.
        /// </summary>
        public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, DateTimeOffset? absoluteExpiration, TimeSpan? slidingExpiration, CancellationToken ct = default)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = absoluteExpiration,
                SlidingExpiration = slidingExpiration,
            };
            await cache.SetAsync(key, value, options, ct);
        }

        /// <summary>
        /// Writes an item to the distributed cache.
        /// The item is serialized in JSON format.
        /// </summary>
        public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options, CancellationToken ct = default)
        {
            var data = System.Text.Json.JsonSerializer.Serialize(value);
            await cache.SetStringAsync(key, data, options, ct);
        }
    }
}
