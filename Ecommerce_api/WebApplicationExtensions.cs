using Ecommerce.Domain.Contracts;

namespace Ecommerce.api
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedAndMigrateAsync(this WebApplication app)
        {

            var scope = app.Services.CreateScope();

            var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Products");
            await seeder.SeedDataAsync();

            var identitySeeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Identity");
            await identitySeeder.SeedDataAsync();


            var deliverySeeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Delivery");
            await deliverySeeder.SeedDataAsync();


            return app;

        }
    }
}
