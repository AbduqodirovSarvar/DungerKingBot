using AutoMapper;
using Dunger.Application.Abstractions.TelegramBotAbstractions;
using Dunger.Application.Mapper;
using Dunger.Application.Services.TelegramServices.TelegramBotKeyboards;
using Dunger.Application.Services.TelegramServices.TelegramBotServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dunger.Application
{
    public static class DepencyInjection
    {
        public static IServiceCollection BotServices(this IServiceCollection _services)
        {
            _services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DepencyInjection).Assembly);
            });
            _services.AddHostedService<ConfigureWebHook>();
            _services.AddScoped<UpdateHandlerService>();
            _services.AddScoped<IReceivedMessageService, ReceivedMessageService>();
            _services.AddScoped<ReplyKeyboards>();
            _services.AddScoped<InlineKeyboards>();
            _services.AddScoped<IRegisterService, RegisterService>();
            _services.AddScoped<ISendMessageService, SendMessageService>();
            _services.AddScoped<IOrderButtonServices, OrderButtonServices>();
            _services.AddScoped<IFeedBackServices, FeedBackServices>();
            _services.AddScoped<IInformationButtonServices, InformationButtonServices>();
            _services.AddScoped<IReceivedCallbackQueryServices, ReceivedCallbackQueryServices>();

            var mappingconfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new Mapping());
            });

            IMapper mapper = mappingconfig.CreateMapper();
            _services.AddSingleton(mapper);

            /*_services.AddScoped<IConnectionMultiplexer>(provider =>
            {
                var config = ConfigurationOptions.Parse(_configuration.GetSection(Redis.Configuration).Value);
                return ConnectionMultiplexer.Connect(config);
            });

            _services.AddScoped<IDatabase>(provider =>
            {
                var connectionMultiplexer = provider.GetRequiredService<IConnectionMultiplexer>();
                return connectionMultiplexer.GetDatabase();
            });*/

            return _services;
        }

    }
}