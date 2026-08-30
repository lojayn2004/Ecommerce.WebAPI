using AutoMapper;
using Ecommerce.Application.Dtos.Products;
using Ecommerce.Domain.Models.Products;
using Microsoft.Extensions.Options;

namespace Ecommerce.Application.AutoMapperProfiles
{
    internal class PictureUrlResolver(IOptions<UrlOptions> _options) : IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            string baseUrl = _options.Value.BaseUrl.TrimEnd('/');
            string path = source.PictureUrl.Trim('/');

            string url = $"{baseUrl}/Files/{path}";
            return url;
        }
    }

    public class UrlOptions
    {
        public string BaseUrl { get; set; }
    }
}
