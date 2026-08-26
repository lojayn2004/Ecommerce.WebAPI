
using Ecommerce.Application.Dtos.DeliveryMethods;
using Ecommerce.Application.Dtos.Order;
using Ecommerce.Application.Dtos.ResultPattern;
using System.Globalization;


namespace Ecommerce.Application.ServicesAbstractions
{
    public interface IOrderService
    {
        Task<Result<OrderDto>> CreateOrder(CreateOrderDto createOrderDto, string userEmail);

        Task<Result<IEnumerable<OrderDto>>> GetUserOrders(string userEmail);

        
        Task<Result<OrderDto>> GetOrderDetails(Guid orderId, string UserEmail);

        Task<Result<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods();
    }
}
