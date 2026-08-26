using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Models.Orders;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.DataSeeding
{
    internal class DeliveryMethodsDataSeeder(StoreDbContext _storeDbContext) : IDataSeeder
    {
        public async Task SeedDataAsync()
        {
            try
            {
                var pendingMigrations = await _storeDbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                    await _storeDbContext.Database.MigrateAsync();

                await SeedingHelper.SeedFileFromEmbeddedResource<DeliveryMethod, int>(_storeDbContext, "DeliveryData.delivery.json");
               
            }
            catch (Exception ex)
            {

            }
        }
      
    }
}
