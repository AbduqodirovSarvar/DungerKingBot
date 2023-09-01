using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dunger.Application.Abstractions
{
    public interface IAppDbContext
    {
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

        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
