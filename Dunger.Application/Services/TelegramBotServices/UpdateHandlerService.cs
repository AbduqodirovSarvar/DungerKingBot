using Dunger.Application.Abstractions;
using Dunger.Application.Abstractions.TelegramBotAbstractions;
using Dunger.Application.Services.TelegramBotMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Dunger.Application.Services.TelegramBotServices
{
    public class UpdateHandlerService
    {
        private readonly ILogger<UpdateHandlerService> _logger;
        private readonly ITelegramBotClient _botclient;
        private readonly IReceivedMessageService _rms;
        private readonly Redis _redis;
        public UpdateHandlerService(ILogger<UpdateHandlerService> logger, ITelegramBotClient client,
            IReceivedMessageService receivedMessageService, Redis redis)
        {
            _logger = logger;
            _botclient = client;
            _rms = receivedMessageService;
            _redis = redis;
        }

        public async Task HandleUpdateAsync(Update update, CancellationToken cancellationToken)
        {
            var handler = update switch
            {
                { Message: { } message } => BotOnMessageReceived(message, cancellationToken),
                { EditedMessage: { } message } => BotOnMessageReceived(message, cancellationToken),
                { CallbackQuery: { } callbackQuery } => BotOnCallbackQueryReceived(callbackQuery, cancellationToken),
                _ => UnknownUpdateHandlerAsync(update, cancellationToken)
            };

            await handler;

            return;
        }

        private async Task BotOnMessageReceived(Message message, CancellationToken cancellationToken)
        {
            string? msg = message!.Text;
            string? state = await _redis.GetUserState(message!.Chat.Id);
            var command = msg switch
            {
                "/start" => _rms.SendStartCommand(_botclient, message, cancellationToken),
                "/help" => _rms.SendHelpCommand(_botclient, message, cancellationToken),
                "Contact" or "Aloqa" or "Контакт" => _rms.ReceivedContactButton(_botclient, message, cancellationToken),
                "Biz haqimizda" or "About Us" or "О нас" => _rms.ReceivedInformationButton(_botclient, message, cancellationToken),
                "Menyu" or "Menu" or "Меню" => _rms.ReceivedMenuButton(_botclient, message, cancellationToken),
                "Buyurtmalarim" or "My Orders" or "Мои заказы" => _rms.ReceivedOrdersButton(_botclient, message, cancellationToken),
                "Fikr bildirish" or "Feedback" or "Обратная связь" => _rms.ReceivedCommentsButton(_botclient, message, cancellationToken),
                "Sozlamalar" or "Settings" or "Настройки" => _rms.ReceivedSettingsButton(_botclient, message, cancellationToken),
                _ when  state != null => _rms.HasStateCommand(_botclient, state, message, cancellationToken),
                _ => _rms.UnknownCommand(_botclient, message, cancellationToken),
            };

            await command;

            return;
        }

        private async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received inline keyboard callback from: {CallbackQueryId}", callbackQuery.Id);

            await _botclient.AnswerCallbackQueryAsync(
                callbackQueryId: callbackQuery.Id,
                text: $"Received {callbackQuery.Data}",
                cancellationToken: cancellationToken);

            await _botclient.SendTextMessageAsync(
                chatId: callbackQuery.Message!.Chat.Id,
                text: $"Received {callbackQuery.Data}",
                cancellationToken: cancellationToken);

            return;
        }

        private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Unknown update type: {UpdateType}, CancellationToken : {}", update.Type, cancellationToken.ToString());
            return Task.CompletedTask;
        }
    }
}
