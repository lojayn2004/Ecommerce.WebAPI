using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Models.Products;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.Infrastructure.DataSeeding
{
    // TODO: NEED TO ADD LOGGING
    internal class ProductsDataSeeder(StoreDbContext _storeDbContext) : IDataSeeder
    {
        public async Task SeedDataAsync()
        {
            try
            {
                var pendingMigrations = await _storeDbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                    await _storeDbContext.Database.MigrateAsync();

                await SeedingHelper.SeedFileFromEmbeddedResource<ProductBrand, int>(_storeDbContext, "ProductsData.brands.json");
                await SeedingHelper.SeedFileFromEmbeddedResource<ProductType, int>(_storeDbContext, "ProductsData.types.json");
                await SeedingHelper.SeedFileFromEmbeddedResource<Product, int>(_storeDbContext, "ProductsData.products.json");


            }
            catch (Exception ex)
            {
              
            }
        }
    }
}
