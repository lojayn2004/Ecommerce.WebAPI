

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.Domain.Models.Orders;

namespace Ecommerce.Infrastructure.Data.Configurations
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.SubTotal)
                   .HasPrecision(8, 2);

         
            builder.OwnsOne(o => o.ShippingAddress, address =>
            {
                address.Property(a => a.FirstName).HasMaxLength(50);
                address.Property(a => a.LastName).HasMaxLength(50);
                address.Property(a => a.Street).HasMaxLength(50);
                address.Property(a => a.City).HasMaxLength(50);
                address.Property(a => a.Country).HasMaxLength(50);
            });

            builder.Property(o => o.UserEmail)
                .HasMaxLength(550);

            builder.Property(o => o.OrderStatus)
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.HasMany(o => o.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.DeliveryMethod)
                .WithMany()
                .HasForeignKey(o => o.DeliveryMethodId);

        }
    }
}
