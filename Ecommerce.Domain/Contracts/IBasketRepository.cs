

using Ecommerce.Domain.Models.Basket;

namespace Ecommerce.Domain.Contracts
{
    public interface  IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId);

        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = default);

        Task<bool> DeleteBasketAsync(string basketId);

    }
}
