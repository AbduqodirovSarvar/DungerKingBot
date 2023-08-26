using Dunger.Application.Abstractions;
using Dunger.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dunger.Infrastructure
{
    public static class DepencyInjection
    {
        public static IServiceCollection InfrastructureServices(this IServiceCollection _services, IConfiguration _configuration)
        {

            _services.AddDbContext<AppDbContext>(options
                => options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));

            _services.AddScoped<IAppDbContext, AppDbContext>();
            return _services;
        }
    }
}