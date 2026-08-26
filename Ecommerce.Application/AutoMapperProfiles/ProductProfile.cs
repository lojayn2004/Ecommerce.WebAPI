using AutoMapper;
using Ecommerce.Application.Dtos.Products;
using Ecommerce.Domain.Models.Products;


namespace Ecommerce.Application.AutoMapperProfiles
{
    internal class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand, BrandDto>();

            CreateMap<ProductType, TypeDto>();

            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductBrand,
                           opt => opt.MapFrom(src => src.ProductBrand))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType));




        }

    }
}
