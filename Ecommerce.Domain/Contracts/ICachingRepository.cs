

namespace Ecommerce.Domain.Contracts
{
    public interface ICachingRepository
    {
        Task<string?> GetAsync(string cachedKey);

        Task SetAsync(string cacheKey, string value, TimeSpan? timeToLive);
    }
}
