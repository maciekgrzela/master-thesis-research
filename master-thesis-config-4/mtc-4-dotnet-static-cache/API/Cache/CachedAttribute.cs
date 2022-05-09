using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace API.Cache
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly StaticCacheValidationTimes _timeToLiveInSeconds;

        public CachedAttribute(StaticCacheValidationTimes timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<RedisCacheSettings>();

            if (!cacheSettings.Enabled)
            {
                await next();
                return;
            }
            
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cachedResponse = await cacheService.GetCacheResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = cachedResponse,
                    ContentType = "application/json"
                };

                context.Result = contentResult;
                return;
            }
            
            var executedContext = await next();

            if (executedContext.Result is OkObjectResult okObjectResult)
            {
                await cacheService.CacheResponseAsync(cacheKey, okObjectResult.Value,
                    TimeSpan.FromSeconds(Convert.ToInt32(_timeToLiveInSeconds)));
            }
        }

        private static string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var builder = new StringBuilder();

            builder.Append($"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(p => p.Key))
            {
                builder.Append($";{key}|{value}");
            }

            return builder.ToString();
        }
    }
}