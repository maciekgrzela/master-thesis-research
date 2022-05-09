using System;
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

                var entities = _commaSeparatedEntities.Split(",");

                foreach (var entity in entities)
                {
                    await cacheService.InvalidateCacheResponseAsync(entity);
                }
            }
        }
    }
}