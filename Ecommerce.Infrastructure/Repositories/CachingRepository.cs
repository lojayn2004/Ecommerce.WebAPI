

using Ecommerce.Domain.Contracts;
using StackExchange.Redis;
using System.Text.Json;

namespace Ecommerce.Infrastructure.Repositories
{
    internal class CachingRepository: ICachingRepository
    {
        private readonly IDatabase _database;

        public CachingRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }
        public async Task<string?> GetAsync(string cachedKey)
        {
             var redisValue = await  _database.StringGetAsync(cachedKey);
            if (redisValue.IsNullOrEmpty)
                return null;
            return redisValue;

        }

        public async Task SetAsync(string cacheKey, string value, TimeSpan? timeToLive)
        {
            await _database.StringSetAsync(cacheKey, value, timeToLive ?? TimeSpan.FromDays(7));
        }
    }
}
