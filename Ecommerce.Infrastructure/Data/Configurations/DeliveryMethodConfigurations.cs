using Ecommerce.Domain.Models.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Data.Configurations
{
    internal class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.ShortName)
                   .HasMaxLength(50);

            builder.Property(d => d.Description)
                   .HasMaxLength(100);

            builder.Property(d => d.DeliveryTime) 
                   .HasMaxLength(50);

            builder.Property(d => d.Price)
                .HasColumnType("DECIMAL(8, 2)");
        }
    }
}
