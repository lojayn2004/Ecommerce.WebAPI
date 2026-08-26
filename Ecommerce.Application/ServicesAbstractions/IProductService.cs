using Ecommerce.Application.Dtos.Products;
using Ecommerce.Application.Dtos.ResultPattern;

namespace Ecommerce.Application.ServicesAbstractions
{
    public interface IProductService
    {
        Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync(ProductQueryParams query);

        Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync();

        Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync();

        Result<ProductDto> GetProductById(int id);

    }
}
