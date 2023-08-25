using Dunger.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Dunger.Application.Services.TelegramBotServices
{
    public class ConfigureWebHook : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly BotConfiguration _botConfig;
        public ConfigureWebHook(ILogger<ConfigureWebHook> logger, IServiceProvider serviceProvider, IOptions<BotConfiguration> configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _botConfig = configuration.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var botclient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
            var webhookAddress = $"{_botConfig.HostAddress}{_botConfig.Route}";

            _logger.LogInformation("Setting webhook: {WebhookAddress}", webhookAddress);
            await botclient.SetWebhookAsync(
                url: webhookAddress,
                allowedUpdates: Array.Empty<UpdateType>(),
                secretToken: _botConfig.SecretKey,
                cancellationToken: cancellationToken);

            await botclient.SendTextMessageAsync(chatId: 636809820, text: "Bot ishladi", cancellationToken: cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

            _logger.LogInformation("Removing webhook");
            await botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
        }
    }
}
