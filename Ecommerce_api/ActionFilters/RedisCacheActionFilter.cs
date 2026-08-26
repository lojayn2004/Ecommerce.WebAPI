using Ecommerce.Application.ServicesAbstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace Ecommerce.api.ActionFilters
{
    public class RedisCacheActionFilter : ActionFilterAttribute 
    {
        private int _cachingDuration;
        public RedisCacheActionFilter(int cachingDuration = 120)
        {
            _cachingDuration = cachingDuration;
        
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ICacheService _cachingService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var requestPath = GetCacheKey(context);
            var cachedData = await _cachingService.GetAsync(requestPath);

            if(cachedData.IsSuccess)
            {
                context.Result = new ContentResult()
                {
                    Content = cachedData.Data,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,

                };
                return;
            }
            var result = await next();
            if(result.Result is OkObjectResult { Value: not null} OK)
            {
               await  _cachingService.SetAsync(requestPath, OK.Value, TimeSpan.FromSeconds(_cachingDuration));
            }
           

        }

        private string GetCacheKey(ActionExecutingContext context)
        {
            var requestPath = new StringBuilder(context.HttpContext.Request.Path);
            var queries = context.HttpContext.Request.Query.OrderBy(x => x.Key);
            requestPath.Append("=");

            foreach (var (key, value) in queries)
            {
                requestPath.Append(key);
                requestPath.Append("?");
                requestPath.Append(value);
                requestPath.Append(";");
            }
            return requestPath.ToString();
        }

    }
}
