using Ecommerce.Application.Dtos.Baskets;
using Ecommerce.Application.ServicesAbstractions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController(IBasketService _basketService): ControllerBase
    {

        [HttpGet("id")] 
        public async Task<IActionResult> GetBasketById(string id)
        {
            var basket = await _basketService.GetBasketAsync(id);
            return Ok(basket);
        }

        [HttpPost] 
        public async Task<IActionResult> CreateOrUpdateBasket(CustomerBasketDto customerBasket, TimeSpan? timeToLive)
        {
            var basket =   await _basketService.CreateOrUpdateBasketAsync(customerBasket, timeToLive);
            return Ok(basket);
        }

        [HttpDelete]
        public async Task<IActionResult>  DeleteBasketById(string id)
        {
            var deleted = await _basketService.DeleteBasketAsync(id);
            return Ok(deleted);

        }
    }
}
