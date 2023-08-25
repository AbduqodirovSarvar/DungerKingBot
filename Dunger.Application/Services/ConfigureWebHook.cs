using Dunger.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Dunger.Application.Services
{
    public class ConfigureWebHook : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        public ConfigureWebHook(ILogger<ConfigureWebHook> logger, IServiceProvider serviceProvider, IConfiguration configuration) 
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var botclient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
            var webhookAddress = $@"{_configuration.GetSection("BotConfig:HostAddress").Value}/bot/{_configuration.GetSection("BotConfig:Token").Value}";
            _logger.LogInformation("Setting web hook");

            await botclient.SetWebhookAsync(
                url: webhookAddress,
                allowedUpdates: Array.Empty<UpdateType>(),
                cancellationToken: cancellationToken);

            await botclient.SendTextMessageAsync(chatId: 636809820, text: "Bot ishladi");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
