using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dunger.Application.EntityTypeConfiguration
{
    public class OrderTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(x => x.Menus).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);
            builder.HasOne(x => x.Filial).WithMany(x => x.Orders).HasForeignKey(x => x.FilialId);
            builder.HasMany(x => x.Payments).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);
        }
    }
}
