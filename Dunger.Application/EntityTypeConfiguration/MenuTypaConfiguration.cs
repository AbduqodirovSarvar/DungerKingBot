using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dunger.Application.EntityTypeConfiguration
{
    public class MenuTypaConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasMany(x => x.Photos).WithOne(x => x.Menu).HasForeignKey(x => x.MenuId);
            builder.HasOne(x => x.Filial).WithMany(x => x.Menus).HasForeignKey(x => x.FilialId);
        }
    }
}
