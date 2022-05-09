using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace API.Cache
{
    public class CacheInvalidationAllowedAttribute : Attribute, IAsyncActionFilter
    {

        private readonly string _commaSeparatedEntities;

        public CacheInvalidationAllowedAttribute(string commaSeparatedEntities)
        {
            _commaSeparatedEntities = commaSeparatedEntities;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<RedisCacheSettings>();

            if (!cacheSettings.Enabled)
            {
                await next();
                return;
            }
            
            var executedContext = await next();

            if (executedContext.Result is CreatedResult or OkObjectResult or NoContentResult)
            {
                var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
                var executionAndInvalidationFrequency = context.HttpContext.RequestServices.GetRequiredService<List<ExecutionAndInvalidationFrequency>>();

                var entities = _commaSeparatedEntities.Split(",");

                foreach (var entity in entities)
                {
                    var entries = executionAndInvalidationFrequency.Where(p => p.EndpointName.Contains(entity));
                    foreach (var entry in entries)
                    {
                        entry.InvalidatorExecutionCount += 1;
                    }
                    
                    await cacheService.InvalidateCacheResponseAsync(entity);
                }
            }
        }
    }
}