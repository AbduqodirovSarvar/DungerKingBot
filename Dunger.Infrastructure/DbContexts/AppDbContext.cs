using Dunger.Application.Abstractions;
using Dunger.Application.EntityTypeConfiguration;
using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dunger.Infrastructure.DbContexts
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Deliver> Delivers { get; set; }
        public DbSet<DeliverPhoto> DeliverPhotos { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderMenu> OrdersMenus { get; set; }
        public DbSet<MenuPhoto> MenuPhotos { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Filial> Filials { get; set; }
        public DbSet<FilialPhotos> FilialPhotos { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<BlockedUser> BlockedUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MenuTypaConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FilialTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DeliverTypeConfiguration());

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(false);
        }
    }
}
