using Dunger.Application.Models;
using Dunger.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Dunger.Application
{
    public static class DepencyInjection
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection _services, IConfiguration _configuration)
        {
            _services.AddHostedService<ConfigureWebHook>();

            //_services.Configure<BotConfiguration>(_configuration.GetSection("BotConfig"));
            _services.AddHttpClient("tgwebhook").AddTypedClient<ITelegramBotClient>(client =>
                new TelegramBotClient(_configuration.GetSection("BotConfig:Token").Value, client));

            _services.AddScoped<UpdateHandlerService>();
            return _services;
        }
    }
}