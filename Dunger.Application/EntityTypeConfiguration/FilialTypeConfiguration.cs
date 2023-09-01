using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dunger.Application.EntityTypeConfiguration
{
    public class FilialTypeConfiguration : IEntityTypeConfiguration<Filial>
    {
        public void Configure(EntityTypeBuilder<Filial> builder)
        {
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasMany(x => x.Photos).WithOne(x => x.Filial).HasForeignKey(x => x.FilialId);
        }
    }
}
