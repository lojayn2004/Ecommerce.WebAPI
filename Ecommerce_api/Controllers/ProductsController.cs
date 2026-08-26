using Ecommerce.api.ActionFilters;
using Ecommerce.Application.Dtos.Products;
using Ecommerce.Application.ServicesAbstractions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(IProductService _productService) : ControllerBase
    {
      
        [HttpGet]
        [RedisCacheActionFilter]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryParams query)
        {
            var products = await _productService.GetAllProductsAsync(query);
            return Ok(products);

        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            return Ok(product);

        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _productService.GetAllBrandsAsync();
            return Ok(brands);
        }


        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {

            var types = await _productService.GetAllTypesAsync();
            return Ok(types);
        }

    }
}
