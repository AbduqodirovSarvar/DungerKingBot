using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dunger.Application.EntityTypeConfiguration
{
    public class MenuTypaConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasOne(x => x.Photo).WithOne().HasForeignKey<Menu>(x => x.PhotoId);
        }
    }
}
