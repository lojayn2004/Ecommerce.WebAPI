using Ecommerce.Domain.Models.Orders;
using Ecommerce.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Data
{
    internal class StoreDbContext: DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }

        protected override  void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<OrderAddress>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<ProductBrand> ProductBrands { get; set; }

        public DbSet<Order> Orderes { get; set; }

        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        
    }
}
