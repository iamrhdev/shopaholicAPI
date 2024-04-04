using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Shopaholic.Application.Abstraction.Caching;
using System.Collections.Concurrent;

namespace Shopaholic.Persistence.Implementations.Caching
{
    public class CacheService : ICacheService
    {
        private static readonly ConcurrentDictionary<string, bool> CacheKeys = new();
        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            string? cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);
            if (cachedValue is null)
                return null;
            T? value = JsonConvert.DeserializeObject<T>(cachedValue);
            return value;
        }

        public async Task<T> GetAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken = default) where T : class
        {
            T? cachedValue = await GetAsync<T>(key, cancellationToken);
            if (cachedValue is not null)
                return cachedValue;

            cachedValue = await factory();
            await SetAsync(key, cachedValue, cancellationToken);
            return cachedValue;
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _distributedCache.RemoveAsync(key, cancellationToken);
            CacheKeys.TryRemove(key, out bool _);
        }

        public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
        {
            //foreach (string key in CacheKeys.Keys)
            //{
            //    if (key.StartsWith(prefixKey))
            //    {
            //        await RemoveAsync(key, cancellationToken);
            //    }
            //}

            IEnumerable<Task> tasks = CacheKeys
                 .Keys
                 .Where(key => key.StartsWith(prefixKey))
                 .Select(key => RemoveAsync(key, cancellationToken));
            await Task.WhenAll(tasks);
        }

        public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
        {
            string cachedValue = JsonConvert.SerializeObject(value);
            await _distributedCache.SetStringAsync(key, cachedValue, cancellationToken);
            CacheKeys.TryAdd(key, false);
        }
    }
}
