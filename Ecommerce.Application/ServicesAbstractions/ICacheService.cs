

using Ecommerce.Application.Dtos.ResultPattern;

namespace Ecommerce.Application.ServicesAbstractions
{
    public interface ICacheService
    {
        Task<Result<string?>> GetAsync(string cachedKey);

        Task<Result> SetAsync(string cacheKey, object value, TimeSpan? timeToLive);
    }
}
