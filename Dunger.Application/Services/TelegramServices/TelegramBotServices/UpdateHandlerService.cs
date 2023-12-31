﻿using Dunger.Application.Abstractions.TelegramBotAbstractions;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace Dunger.Application.Services.TelegramServices.TelegramBotServices
{
    public class UpdateHandlerService
    {
        private readonly ILogger<UpdateHandlerService> _logger;
        private readonly IReceivedMessageService _receivedMessageService;
        private readonly IReceivedCallbackQueryServices _queryServices;
        public UpdateHandlerService(ILogger<UpdateHandlerService> logger, IReceivedCallbackQueryServices receivedCallbackQueryServices,
            IReceivedMessageService receivedMessageService)
        {
            _logger = logger;
            _receivedMessageService = receivedMessageService;
            _queryServices = receivedCallbackQueryServices;
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
            string? state = Redis.GetUserState(message.Chat.Id);

            if (state == null)
            {
                await _receivedMessageService.CatchMessageWithoutState(message, cancellationToken);
                return;
            }

            await _receivedMessageService.CatchMessageWithState(message, state, cancellationToken);

            return;
        }

        private async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Received inline keyboard callback from: {CallbackQueryId}", callbackQuery.Id);

            var state = Redis.GetUserState(callbackQuery.From.Id);

            if (state == null)
            {
                await _queryServices.CatchCallbackQueryWithoutState(callbackQuery, cancellationToken);
                return;
            }

            await _queryServices.CatchCallbackQueryWithState(callbackQuery, state, cancellationToken);

            return;
        }

        private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Unknown update type: {UpdateType}, CancellationToken : {}", update.Type, cancellationToken.ToString());
            return Task.CompletedTask;
        }
    }
}
