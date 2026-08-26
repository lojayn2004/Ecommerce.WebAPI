

using AutoMapper;
using Ecommerce.Application.Dtos.Baskets;
using Ecommerce.Application.Dtos.ResultPattern;
using Ecommerce.Application.ServicesAbstractions;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Models.Basket;

namespace Ecommerce.Application.Services
{
    public class BasketService(IBasketRepository _basketRepo, IMapper _mapper): IBasketService
    {
        public async Task<Result<CustomerBasketDto>> CreateOrUpdateBasketAsync(CustomerBasketDto basket,
            TimeSpan? TimeToLive)
        {
            var customerBasket = _mapper.Map<CustomerBasket>(basket);
            var createdBasket = await _basketRepo.CreateOrUpdateBasketAsync(customerBasket, TimeToLive);
            if (createdBasket == null)
                return Errors.FailedBasketCreation;
            return Result<CustomerBasketDto>.Success(basket);
        }

        public async Task<Result> DeleteBasketAsync(string basketId)
        {
            var isDeleted= await _basketRepo.DeleteBasketAsync(basketId);
            if (!isDeleted)
                return Errors.DeleteBasketFailed;
            return Result.Success();
        }

        public async Task<Result<CustomerBasketDto>> GetBasketAsync(string basketId)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);
            // add more clear message like basket with id ... is not found 
            if (basket == null)
                return Errors.BasketNotFound;
            return Result<CustomerBasketDto>.Success(_mapper.Map<CustomerBasketDto>(basket));
        }
    }
}
