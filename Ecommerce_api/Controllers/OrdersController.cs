using Ecommerce.Application.Dtos.Order;
using Ecommerce.Application.ServicesAbstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IOrderService _orderService): ApiBaseController
    {
        [Authorize]
        [HttpPost] 
        public async Task<IActionResult> CreateOrder(CreateOrderDto orderDto)
        {
            var order = await _orderService.CreateOrder(orderDto, base.UserEmail);
            return ToActionResult(order);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserOrders()
        {
            var orders = await _orderService.GetUserOrders(base.UserEmail);
            return ToActionResult(orders);
        }

        [Authorize]
        [HttpGet("{orderId:guid}")]
        public async Task<IActionResult> GetOrderDetails(Guid orderId)
        {
            var orderDetails = await _orderService.GetOrderDetails(orderId, base.UserEmail);
            return ToActionResult(orderDetails);
        }
        [HttpGet("delivery-methods")] 
        public async Task<IActionResult> GetDeliveryMethods()
        {
            var deliveryMethods = await _orderService.GetDeliveryMethods();

            return ToActionResult(deliveryMethods);
        }
    }
}
