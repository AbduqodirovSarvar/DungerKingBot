
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
        private readonly ReplyKeyboards _replyKeyboard;
        private readonly IAppDbContext _context;
        private readonly IRegisterService _registerService;
        public ReceivedMessageService(ReplyKeyboards keyboard, IAppDbContext context, IRegisterService registerService)
        {
            _replyKeyboard = keyboard;
            _context = context;
            _registerService = registerService;
        }
        public async Task SendStartCommand(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            Domain.Entities.User user = new Domain.Entities.User();
            try
            {
                user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == message.Chat.Id, cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (user == null)
            {
                await _registerService.CatchMessage(botClient, message, cancellationToken);
                return;
            }

            await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                text: $"Assalomu aleykum {user.FirstName}\nBotimizga hush kelibsiz!",
                replyMarkup: _replyKeyboard.BotOnReceivedStart(),
                cancellationToken: cancellationToken);
        }
        public async Task SendHelpCommand(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(chatId: message.Chat.Id, text: "Assalomu aleykum\nBotimizdan foydalanish uchun /start buyrug'ini bosing!", cancellationToken: cancellationToken);
        }

        public async Task Usage(ITelegramBotClient botClient, string State, Message message, CancellationToken cancellationToken)
        {
            var state = State switch
            {
                "Language" => _registerService.SendLanguage(botClient, RegisterService.user, message, cancellationToken),
                "FirstName" => _registerService.SendFirstName(botClient, RegisterService.user, message, cancellationToken),
                "LastName" => _registerService.SendLastName(botClient, RegisterService.user, message, cancellationToken),
                "Contact" => _registerService.SendContact(botClient, RegisterService.user, message, cancellationToken),
                _ => _registerService.CatchMessage(botClient, message, cancellationToken)
            };

            await state;
        }

    }
}
