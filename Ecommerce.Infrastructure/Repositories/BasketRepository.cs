

using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Models.Basket;
using StackExchange.Redis;
using System.Text.Json;


namespace Ecommerce.Infrastructure.Repositories
{
    internal class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            var basketJson = JsonSerializer.Serialize(basket);
            var isCreated = await _database.StringSetAsync(basket.Id, basketJson, timeToLive ?? TimeSpan.FromDays(7));

            return isCreated ? basket : null;

        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basketRedisValue = await _database.StringGetAsync(basketId);
            if(basketRedisValue.IsNullOrEmpty)
                return null;
            return JsonSerializer.Deserialize<CustomerBasket>(basketRedisValue!);
        }
    }
}
