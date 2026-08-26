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
            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserOrders()
        {
            var orders = await _orderService.GetUserOrders(base.UserEmail);
            return Ok(orders);
        }

        [HttpGet("{orderId:guid}")]
        public async Task<IActionResult> GetOrderDetails(Guid orderId)
        {
            var deliveryMethods = await _orderService.GetOrderDetails(orderId, base.UserEmail);
            return Ok();
        }
        [HttpPost("delivery-methods")] 
        public async Task<IActionResult> GetDeliveryMethods()
        {
            var deliveryMethods = await _orderService.GetDeliveryMethods();

            return Ok(deliveryMethods);
        }
    }
}
