using AutoMapper;
using Ecommerce.Application.Dtos.DeliveryMethods;
using Ecommerce.Application.Dtos.Order;
using Ecommerce.Domain.Models.Orders;

namespace Ecommerce.Application.AutoMapperProfiles
{
    class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderItem, OrderItemDto>();

            CreateMap<DeliveryMethod, DeliveryMethodDto>();
        }
    }
}
