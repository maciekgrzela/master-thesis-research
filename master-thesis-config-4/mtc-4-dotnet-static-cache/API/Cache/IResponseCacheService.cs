using System;
using System.Threading.Tasks;

namespace API.Cache
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string key, object response, TimeSpan timeToLive);
        Task<string> GetCacheResponseAsync(string key);
        Task InvalidateCacheResponseAsync(string entityKeyPart);
    }
}