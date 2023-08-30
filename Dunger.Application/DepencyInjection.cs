using Dunger.Application.Abstractions.TelegramBotAbstractions;
using Dunger.Application.Services.TelegramBotKeyboards;
using Dunger.Application.Services.TelegramBotServices;
using Dunger.Application.Services.TelegramBotStates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Dunger.Application
{
    public static class DepencyInjection
    {
        public static IServiceCollection BotServices(this IServiceCollection _services, IConfiguration _configuration)
        {
            _services.AddHostedService<ConfigureWebHook>();
            _services.AddScoped<UpdateHandlerService>();
            _services.AddScoped<IReceivedMessageService, ReceivedMessageService>();
            _services.AddScoped<ReplyKeyboards>();
            _services.AddSingleton<Redis>();
            _services.AddScoped<InlineKeyboards>();
            _services.AddScoped<IRegisterService, RegisterService>();
            _services.AddScoped<ISendMessageService, SendMessageService>();
            _services.AddScoped<IOrderButtonServices, OrderButtonServices>();
            _services.AddScoped<IFeedBackServices, FeedBackServices>();
            _services.AddScoped<IInformationButtonServices, InformationButtonServices>();
            _services.AddScoped<IReceivedCallbackQueryServices, ReceivedCallbackQueryServices>();

            _services.AddSingleton<IConnectionMultiplexer>(provider =>
            {
                var config = ConfigurationOptions.Parse(_configuration.GetSection(Redis.Configuration).Value);
                return ConnectionMultiplexer.Connect(config);
            });

            _services.AddSingleton<IDatabase>(provider =>
            {
                var connectionMultiplexer = provider.GetRequiredService<IConnectionMultiplexer>();
                return connectionMultiplexer.GetDatabase();
            });

            return _services;
        }

    }
}