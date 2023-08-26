
using Dunger.Application.Abstractions;
using Dunger.Application.Abstractions.TelegramBotAbstractions;
using Dunger.Application.Services.TelegramBotKeyboards;
using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Dunger.Application.Services.TelegramBotServices
{
    public class ReceivedMessageService : IReceivedMessageService
    {
        private readonly IAppDbContext _context;
        private readonly IRegisterService _registerService;
        public ReceivedMessageService(IAppDbContext context, IRegisterService registerService)
        {
            _context = context;
            _registerService = registerService;
        }
        public async Task SendStartCommand(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(x => x.TelegramId == message.Chat.Id);

            if (user == null)
            {
                await _registerService.CatchMessage(botClient, message, cancellationToken);
                return;
            }

            await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                text: $"Assalomu aleykum {user.FirstName}\nBotimizga hush kelibsiz!",
                replyMarkup: ReplyKeyboards.BotOnReceivedStart,
                cancellationToken: cancellationToken);

            return;
        }
        public async Task SendHelpCommand(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(chatId: message.Chat.Id, text: "Assalomu aleykum\nBotimizdan foydalanish uchun /start buyrug'ini bosing!", cancellationToken: cancellationToken);

            return;
        }

        public async Task Usage(ITelegramBotClient botClient, string State, Message message, CancellationToken cancellationToken)
        {
            var state = State switch
            {
                "Language" => _registerService.SendLanguage(botClient, RegisterService.UserObject!, message, cancellationToken),
                "FirstName" => _registerService.SendFirstName(botClient, RegisterService.UserObject!, message, cancellationToken),
                "LastName" => _registerService.SendLastName(botClient, RegisterService.UserObject!, message, cancellationToken),
                "Contact" => _registerService.SendContact(botClient, RegisterService.UserObject!, message, cancellationToken),
                _ => _registerService.CatchMessage(botClient, message, cancellationToken)
            };

            await state;

            return;
        }

    }
}
