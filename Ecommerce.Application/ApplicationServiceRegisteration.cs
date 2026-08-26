using Ecommerce.Application.AutoMapperProfiles;
using Ecommerce.Application.Services;
using Ecommerce.Application.ServicesAbstractions;
using Microsoft.Extensions.DependencyInjection;


namespace Ecommerce.Application
{
    public static  class ApplicationServiceRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductProfile));

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;

        }
    }
}
