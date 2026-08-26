using Ecommerce.Domain.Models.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Data.Configurations
{
    internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {

            builder.Property(a => a.Price)
                .HasColumnType("decimal(18, 2)");
              
            builder.Property(a => a.PictureUrl)
                .HasMaxLength(150);
            builder.Property(a => a.ProductName)
               .HasMaxLength(50);
        

        }
    }
}
