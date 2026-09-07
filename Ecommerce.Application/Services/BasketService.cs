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
     
                return Result<CustomerBasketDto>.Failure(ErrorType.Server, "Basket Creation Failed");
           
            return Result<CustomerBasketDto>.Success(basket);
        }

        public async Task<Result<string>> DeleteBasketAsync(string basketId)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);
           
            if (basket == null)
                return Result<string>.Failure(ErrorType.NotFound, $"Basket With Id {basketId} Not Found");
            var isDeleted= await _basketRepo.DeleteBasketAsync(basketId);
            if (!isDeleted)
                return Result<string>.Failure(ErrorType.Server, "Basket Deletion Failed");
            return Result<string>.Success("Deleted Successfully");
        }

        public async Task<Result<CustomerBasketDto>> GetBasketAsync(string basketId)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);
          
            if (basket == null)
                return Result<CustomerBasketDto>.Failure(ErrorType.NotFound, $"Basket With Id {basketId} Not Found");
            return Result<CustomerBasketDto>.Success(_mapper.Map<CustomerBasketDto>(basket));
        }
    }
}
