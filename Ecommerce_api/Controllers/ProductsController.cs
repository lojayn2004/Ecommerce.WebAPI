using Ecommerce.api.ActionFilters;
using Ecommerce.api.Controllers;
using Ecommerce.Application.Dtos.Products;
using Ecommerce.Application.ServicesAbstractions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(IProductService _productService) : ApiBaseController
    {
      
        [HttpGet]
        [RedisCacheActionFilter]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryParams query)
        {
            var products = await _productService.GetAllProductsAsync(query);
            return ToActionResult(products);

        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            return ToActionResult(product);

        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _productService.GetAllBrandsAsync();
            return ToActionResult(brands);
        }


        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {

            var types = await _productService.GetAllTypesAsync();
            return ToActionResult(types);
        }

    }
}
