using Dunger.Application.Abstractions;
using Dunger.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dunger.Infrastructure
{
    public static class DepencyInjection
    {
        public static IServiceCollection InfrastructureServices(this IServiceCollection _services, IConfiguration _configuration)
        {

            _services.AddDbContextFactory<AppDbContext>(opt => opt.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));

            _services.AddDbContext<AppDbContext>(options
                => options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));

            _services.AddScoped<IAppDbContext, AppDbContext>();

            _services.AddSingleton<IDesignTimeDbContextFactory<AppDbContext>>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                return new AppDbContextFactory();
            });

            return _services;
        }
    }
}