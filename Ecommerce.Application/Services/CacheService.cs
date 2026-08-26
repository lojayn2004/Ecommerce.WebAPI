

using Ecommerce.Application.Dtos.Baskets;
using Ecommerce.Application.Dtos.ResultPattern;
using Ecommerce.Application.ServicesAbstractions;
using Ecommerce.Domain.Contracts;
using System.Text.Json;

namespace Ecommerce.Application.Services
{
    public class CacheService(ICachingRepository _cacheRepo) : ICacheService
    {
        public async Task<Result<string?>> GetAsync(string cachedKey)
        {
            var cachedString = await  _cacheRepo.GetAsync(cachedKey);
            if (cachedString == null)
                return Result<string?>.Failure(ErrorType.NotFound, $"Cached Key {cachedKey} Not Found");
            return Result<string?>.Success(cachedString);

        }

        public async Task<Result<string>> SetAsync(string cacheKey, object value, TimeSpan? timeToLive)
        {
            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var valueStrJson = JsonSerializer.Serialize(value, jsonOptions);
            await _cacheRepo.SetAsync(cacheKey, valueStrJson, timeToLive);
            return Result<string>.Success("Cached Item Successfully");
        }

    }
}
