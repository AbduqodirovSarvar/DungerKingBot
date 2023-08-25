using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Dunger.Application.Services
{
    public class UpdateHandlerService
    {
        private readonly ILogger<UpdateHandlerService> _logger;
        private readonly ITelegramBotClient _botclient;
        public UpdateHandlerService(ILogger<UpdateHandlerService> logger, ITelegramBotClient client) 
        {
            _logger = logger;
            _botclient = client;
        }

        public async Task EchoAsync(Update update)
        {
            if (update == null)
            {
                throw new Exception();
            }
            var handler = update.Type switch
            {
                UpdateType.Message => ReceivedMessage(update.Message),
                UpdateType.CallbackQuery => ReceivedCallBackQuery(update.CallbackQuery),
                _ => UnKnownMessage(update)
            };
            try
            {
                await handler;
            }
            catch (Exception ex)
            {
                await HandlerError(ex);
            }
        }


        public async Task ReceivedMessage(Message message)
        {
            _logger.LogInformation($"Botga {message.Chat.FirstName} dan {message.Type} keldi: ");

            await _botclient.SendTextMessageAsync(chatId: message.Chat.Id, text: "Assalomu aleykum");
        }

        public Task ReceivedCallBackQuery(CallbackQuery query)
        {
            return Task.CompletedTask;
        }

        public Task UnKnownMessage(Update update)
        {
            _logger.LogInformation("Belgilanmagan turdagi xabar keldi");
            return Task.CompletedTask;
        }

        public Task HandlerError(Exception ex)
        {
            _logger.LogInformation("Handler ishlamadi");

            return Task.CompletedTask;
        }
    }
}
