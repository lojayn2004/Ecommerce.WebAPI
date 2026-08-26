

using AutoMapper;
using Ecommerce.Application.Dtos.Baskets;
using Ecommerce.Domain.Models.Basket;

namespace Ecommerce.Application.AutoMapperProfiles
{
    internal class BasketProfile: Profile
    {
        public BasketProfile()
        {

            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();

            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

        }
    }
}
