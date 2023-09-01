using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.EntityTypeConfiguration
{
    public class DeliverTypeConfiguration : IEntityTypeConfiguration<Deliver>
    {
        public void Configure(EntityTypeBuilder<Deliver> builder)
        {
            builder.HasIndex(x => x.Phone).IsUnique();
            builder.HasOne(x => x.DeliverPhoto).WithOne(x => x.Deliver).HasForeignKey<Deliver>(x => x.PhotoId);
            builder.HasIndex(x => x.Phone).IsUnique();
            builder.HasIndex(x => x.VehicleNumber).IsUnique();
        }
    }
}
