using Ecommerce.Application.Dtos.Order;
using Ecommerce.Application.ServicesAbstractions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IOrderService _orderService): ApiBaseController
    {
        [HttpPost] 
        public async Task<IActionResult> CreateOrder(CreateOrderDto orderDto)
        {
            var order = await _orderService.CreateOrder(orderDto, base.UserEmail);
            return ToActionResult(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserOrders()
        {
            var orders = await _orderService.GetUserOrders(base.UserEmail);
            return ToActionResult(orders);
        }

        [HttpGet("{orderId:guid}")]
        public async Task<IActionResult> GetOrderDetails(Guid orderId)
        {
            var orderDetails = await _orderService.GetOrderDetails(orderId, base.UserEmail);
            return ToActionResult(orderDetails);
        }
        [HttpPost("delivery-methods")] 
        public async Task<IActionResult> GetDeliveryMethods()
        {
            var deliveryMethods = await _orderService.GetDeliveryMethods();

            return ToActionResult(deliveryMethods);
        }
    }
}
