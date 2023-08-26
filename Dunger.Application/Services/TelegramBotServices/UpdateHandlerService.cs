using Dunger.Application.Abstractions.TelegramBotAbstractions;
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
        public UpdateHandlerService(ILogger<UpdateHandlerService> logger, ITelegramBotClient client, IReceivedMessageService receivedMessageService, Redis redis)
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
                { InlineQuery: { } inlineQuery } => BotOnInlineQueryReceived(inlineQuery, cancellationToken),
                { ChosenInlineResult: { } chosenInlineResult } => BotOnChosenInlineResultReceived(chosenInlineResult, cancellationToken),
                _ => UnknownUpdateHandlerAsync(update, cancellationToken)
            };

            await handler;
        }

        private async Task BotOnMessageReceived(Message message, CancellationToken cancellationToken)
        {
            string? state = await _redis.GetUserState(message.Chat.Id);

            var action = message.Text switch
            {
                "/start" => _rms.SendStartCommand(_botclient, message, cancellationToken),
                "/help" => _rms.SendHelpCommand(_botclient, message, cancellationToken),
                _ when state != null => _rms.Usage(_botclient, state, message, cancellationToken),
                _ => UnknownCommand()
            };
        }

        private Task UnknownCommand()
        {
            throw new NotImplementedException();
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
        }

        private Task BotOnChosenInlineResultReceived(ChosenInlineResult chosenInlineResult, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private Task BotOnInlineQueryReceived(InlineQuery inlineQuery, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
            return Task.CompletedTask;
        }
    }
}
