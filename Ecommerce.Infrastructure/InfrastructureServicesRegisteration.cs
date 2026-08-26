

using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Models.Identity;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.DataSeeding;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Ecommerce.Infrastructure
{
    public  static class InfrastructureServicesRegisteration
    {
        public static IServiceCollection AddInfraStructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("IdentityDbConnection"));
            });

            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();


            services.AddKeyedScoped<IDataSeeder, ProductsDataSeeder>("Products");
            services.AddKeyedScoped<IDataSeeder, IdentityDataSeeder>("Identity");
            services.AddKeyedScoped<IDataSeeder, DeliveryMethodsDataSeeder>("Delivery");


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!);
            });

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICachingRepository, CachingRepository>();
            
            return services;


        }
    }
}
