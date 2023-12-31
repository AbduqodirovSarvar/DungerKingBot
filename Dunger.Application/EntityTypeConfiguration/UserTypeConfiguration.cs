﻿using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dunger.Application.EntityTypeConfiguration
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(x => x.Comments).WithOne(x => x.User).HasForeignKey(x => x.TelegramId);
            builder.HasKey(x => x.TelegramId);
            builder.HasIndex(x => x.Phone).IsUnique();
            builder.HasMany(x => x.Orders).WithOne(x => x.User).HasForeignKey(x => x.TelegramId);
        }
    }
}
