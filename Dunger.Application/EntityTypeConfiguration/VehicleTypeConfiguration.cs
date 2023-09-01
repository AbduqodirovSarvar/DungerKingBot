using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dunger.Application.EntityTypeConfiguration
{
    public class VehicleTypeConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.Delivers).WithOne(x => x.Vehicle).HasForeignKey(x => x.VehicleId);
        }
    }
}
