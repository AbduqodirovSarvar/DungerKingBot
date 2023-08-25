using Dunger.Application.Abstractions;
using Dunger.Application.Models;
using Dunger.Application.Services.TelegramBotServices;
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
            return _services;
        }

        public static IServiceCollection BotServices(this IServiceCollection _services, IConfiguration _configurations)
        {
            _services.AddHostedService<ConfigureWebHook>();
            _services.AddScoped<UpdateHandlerService>();
            _services.AddScoped<IReceivedMessageService, ReceivedMessageService>();
            return _services;
        }
    }
}
