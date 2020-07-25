using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using OpenTracing;
using System.Threading;
using System.Threading.Tasks;

namespace Hcqn.Caching
{
    public class Cache : ICache
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<Cache> _logger;
        private readonly ITracer _tracer;

        public Cache(IDistributedCache distributedCache, ILogger<Cache> logger, ITracer tracer)
        {
            _distributedCache = distributedCache;
            _logger = logger;
            _tracer = tracer;
        }

        public async Task<T> Get<T>(string key, CancellationToken token = default) where T : class
        {
            using var scope = _tracer.BuildSpan("Cache.Get").StartActive(true);
            _logger.LogDebug("cache", "obtendo cache para a chave {key}", key);
            var cache = await _distributedCache.GetAsync(key, token).ConfigureAwait(false);
            return cache.FromByteArray<T>();
        }

        public Task Refresh(string key, CancellationToken token = default)
        {
            using var scope = _tracer.BuildSpan("Cache.Refresh").StartActive(true);
            _logger.LogDebug("cache", "Atualizando um valor no cache com a chave {key}", key);
            return _distributedCache.RefreshAsync(key, token);
        }

        public Task Remove(string key, CancellationToken token = default)
        {
            using var scope = _tracer.BuildSpan("Cache.Remove").StartActive(true);
            _logger.LogDebug("cache", "Removendo um valor no cache com a chave '{key}'", key);
            return _distributedCache.RemoveAsync(key, token);
        }

        public async Task Set<T>(string key, T value, CacheEntryOptions options, CancellationToken token = default)
        {
            using var scope = _tracer.BuildSpan("Cache.Set").StartActive(true);
            _logger.LogDebug("cache", "Adicionando cache para a chave {key}", key);
            var byteArray = value.ToByteArray();
            await _distributedCache.SetAsync(key, byteArray, token).ConfigureAwait(false);
        }
    }
}
