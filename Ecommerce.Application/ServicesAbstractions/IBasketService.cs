

using Ecommerce.Application.Dtos.Baskets;
using Ecommerce.Application.Dtos.ResultPattern;

namespace Ecommerce.Application.ServicesAbstractions
{
    public interface IBasketService
    {
        Task<Result<CustomerBasketDto>> GetBasketAsync(string basketId);


        Task<Result<string>> DeleteBasketAsync(string basketId);

        Task<Result<CustomerBasketDto>> CreateOrUpdateBasketAsync(CustomerBasketDto basket, TimeSpan? TimeToLive);
    }

}
