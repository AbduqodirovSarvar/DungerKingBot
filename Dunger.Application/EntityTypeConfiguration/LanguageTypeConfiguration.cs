using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dunger.Application.EntityTypeConfiguration
{
    public class LanguageTypeConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasData(new Language
            {
                Id = 1,
                Name = "uz",
                CreatedTime = DateTime.SpecifyKind(DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime, DateTimeKind.Utc).ToUniversalTime()
            },
            new Language
            {
                Id = 2,
                Name = "en",
                CreatedTime = DateTime.SpecifyKind(DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime, DateTimeKind.Utc).ToUniversalTime()
            },
            new Language
            {
                Id = 3,
                Name = "ru",
                CreatedTime = DateTime.SpecifyKind(DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime, DateTimeKind.Utc).ToUniversalTime()
            });
        }
    }
}
