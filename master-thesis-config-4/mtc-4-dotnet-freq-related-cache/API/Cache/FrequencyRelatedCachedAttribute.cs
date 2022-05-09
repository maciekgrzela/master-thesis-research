using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using API.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace API.Cache
{
    public class FrequencyRelatedCachedAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<RedisCacheSettings>();

            if (!cacheSettings.Enabled)
            {
                await next();
                return;
            }
            
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var cachingOptions = context.HttpContext.RequestServices.GetRequiredService<IOptions<CachingOptions>>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cachedResponse = await cacheService.GetCacheResponseAsync(cacheKey);

            var executionAndInvalidationFrequency = context.HttpContext.RequestServices
                .GetRequiredService<List<ExecutionAndInvalidationFrequency>>();

            var entry = executionAndInvalidationFrequency.FirstOrDefault(p => p.EndpointName == cacheKey);
            var executionCount = 1;
            var invalidatorCount = 0;

            if (entry == null)
            {
                executionAndInvalidationFrequency.Add(new ExecutionAndInvalidationFrequency
                {
                    EndpointName = cacheKey,
                    ExecutionCount = executionCount,
                    InvalidatorExecutionCount = invalidatorCount
                });
            }
            else
            {
                executionCount = entry.ExecutionCount + 1;
                invalidatorCount = entry.InvalidatorExecutionCount;
                entry.ExecutionCount = executionCount;
            }

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
                var maxExecution = executionAndInvalidationFrequency.Max(p => p.ExecutionCount);
                var maxInvalidation = executionAndInvalidationFrequency.Max(p => p.InvalidatorExecutionCount);
                var executionFactor = maxExecution == 0 ? 1 : Convert.ToDouble(executionCount / maxExecution);
                var invalidationFactor = maxInvalidation == 0 ? 0 : Convert.ToDouble(invalidatorCount / maxInvalidation);

                var timeToLive = ((0.5 * executionFactor) + (0.5 * (1 - invalidationFactor))) * cachingOptions.Value.MaximumTimeToLive;

                await cacheService.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(Convert.ToInt32(timeToLive)));
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