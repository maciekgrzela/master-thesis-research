using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace API.Cache
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public ResponseCacheService(IDistributedCache distributedCache, IConnectionMultiplexer connectionMultiplexer)
        {
            _distributedCache = distributedCache;
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task CacheResponseAsync(string key, object response, TimeSpan timeToLive)
        {
            if (response == null)
            {
                return;
            }

            var serializedResponse = JsonConvert.SerializeObject(response);

            await _distributedCache.SetStringAsync(key, serializedResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
        }

        public async Task<string> GetCacheResponseAsync(string key)
        {
            var cachedResponse = await _distributedCache.GetStringAsync(key);
            return string.IsNullOrEmpty(cachedResponse) ? default : cachedResponse;
        }

        public async Task InvalidateCacheResponseAsync(string entityKeyPart)
        {
            var redisServer = _connectionMultiplexer.GetServer("localhost", 6379);
            
            var entityRelatedKeys = redisServer.Keys(pattern: $"*{entityKeyPart}*").Select(key => key.ToString()).ToList();

            foreach (var entityRelatedKey in entityRelatedKeys)
            {
                var cachedResponse = await _distributedCache.GetStringAsync(entityRelatedKey);

                if (string.IsNullOrEmpty(cachedResponse))
                {
                    continue;
                }
                
                await _distributedCache.RemoveAsync(entityRelatedKey);
            }
        }
    }
}