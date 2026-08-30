using AutoMapper;
using Ecommerce.Application.Dtos.Products;
using Ecommerce.Application.Dtos.ResultPattern;
using Ecommerce.Application.ServicesAbstractions;
using Ecommerce.Application.Specifications;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Models.Products;

namespace Ecommerce.Application.Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();

            var mappedBrands = _mapper.Map<IReadOnlyList<BrandDto>>(brands);

            return Result<IReadOnlyList<BrandDto>>.Success(mappedBrands);
        }

        public async Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync(ProductQueryParams query)
        {
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(new ProductWithBrandAndTypeSpecification(query));

            Console.WriteLine("Productssssssssssssssssssssssssssss");
            foreach (var product in products) Console.WriteLine("Name: " + product.Name);
           

            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            return Result<IReadOnlyList<ProductDto>>.Success(mappedProducts);
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();

            var mappedTypes = _mapper.Map<IReadOnlyList<TypeDto>>(types);
            return Result<IReadOnlyList<TypeDto>>.Success(mappedTypes);
        }

        public Result<ProductDto> GetProductById(int id)
        {
            var product = _unitOfWork.GetRepository<Product, int>().GetById(new ProductWithBrandAndTypeSpecification(id));

            if (product == null)
                return Result<ProductDto>.Failure(ErrorType.NotFound, $"Basket With Id {id} Doesnot Have Items");
            return Result<ProductDto>.Success(_mapper.Map<ProductDto>(product));
        }
    }
}
