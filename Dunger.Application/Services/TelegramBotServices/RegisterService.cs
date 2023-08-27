using Dunger.Application.Abstractions;
using Dunger.Application.Abstractions.TelegramBotAbstractions;
using Dunger.Application.Services.TelegramBotKeyboards;
using Dunger.Application.Services.TelegramBotMessages;
using Dunger.Application.Services.TelegramBotStates;
using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Dunger.Application.Services.TelegramBotServices
{
    public class RegisterService : IRegisterService
    {
        private readonly Redis _redis;
        private readonly ReplyKeyboards _keyboards;
        private readonly IAppDbContext _context;
        private readonly RegisterState _state;
        public RegisterService(Redis redis, ReplyKeyboards keyboards, IAppDbContext context, RegisterState state)
        {
            _redis = redis;
            _keyboards = keyboards;
            _context = context;
            _state = state;
        }
        public static Domain.Entities.User? UserObject { get; set; }
        public async Task CatchMessage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            var state = await _redis.GetUserState(message.Chat.Id);
            if (state == null || UserObject == null)
            {
                await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: $"Tilni tanglang!\nChoose language!\nВыберите язык!",
                    replyMarkup: _keyboards.LanguageKeyboard,
                    cancellationToken: cancellationToken);

                await _redis.SetUserState(message.Chat.Id, RegisterState.States[0]);

                UserObject = new Domain.Entities.User
                {
                    UserName = message.Chat.Username,
                    TelegramId = message.Chat.Id
                };

                return;
            }
        }

        public async Task SendContact(ITelegramBotClient botClient, Domain.Entities.User user, Message message, CancellationToken cancellationToken)
        {
            if (user == null || message.Contact == null || message.Contact.PhoneNumber == null)
            {
                return;
            }

            user.Phone = message.Contact.PhoneNumber;

            await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: ReplyMessages.afterRegistered[user.LanguageId],
                    replyMarkup: ReplyKeyboards.MainPageKeyboards[user.LanguageId],
                    cancellationToken: cancellationToken);

            await _state.Finished(message.Chat.Id);

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            UserObject = null;

            return;
        }

        public async Task SendFirstName(ITelegramBotClient botClient, Domain.Entities.User user, Message message, CancellationToken cancellationToken)
        {
            user.FirstName = message.Text ?? $"{message.Chat.FirstName}";

            await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: ReplyMessages.askLastName[user.LanguageId],
                    cancellationToken: cancellationToken);

            await _state.Next(message.Chat.Id);

            return;
        }

        public async Task SendLastName(ITelegramBotClient botClient, Domain.Entities.User user, Message message, CancellationToken cancellationToken)
        {
            user.LastName = message.Text ?? $"{message.Chat.LastName}";

            ReplyKeyboardMarkup replyKeyboard = new(
                new[]
                {
                    new KeyboardButton[] { new KeyboardButton(ReplyMessages.shareContact[user.LanguageId])
                    { RequestContact = true } }
                })
                {
                    ResizeKeyboard = true
                };

            await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: ReplyMessages.askContact[user.LanguageId],
                    replyMarkup: replyKeyboard,
                    cancellationToken: cancellationToken);

            await _state.Next(message.Chat.Id);

            return;
        }

        public async Task SendLanguage(ITelegramBotClient botClient, Domain.Entities.User user, Message message, CancellationToken cancellationToken)
        {
            Language? lang = await _context.Languages.FirstOrDefaultAsync(x => x.Name == message.Text!.ToString(), cancellationToken);
            user.LanguageId = lang?.Id ?? 1;

            await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: ReplyMessages.askFirstName[user.LanguageId],
                    replyMarkup: new ReplyKeyboardRemove(),
                    cancellationToken: cancellationToken);

            await _state.Next(message.Chat.Id);

            return;
        }
    }
}
