using Dunger.Application.Abstractions;
using Dunger.Application.Abstractions.TelegramBotAbstractions;
using Dunger.Application.Services.TelegramServices.TelegramBotKeyboards;
using Dunger.Application.Services.TelegramServices.TelegramBotMessages;
using Dunger.Application.Services.TelegramServices.TelegramBotStates;
using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Dunger.Application.Services.TelegramServices.TelegramBotServices
{
    public class RegisterService : IRegisterService
    {
        private readonly IAppDbContext _context;
        private readonly ITelegramBotClient _client;
        private readonly ILogger<RegisterService> _logger;
        private readonly ISendMessageService _sendMessageService;
        public RegisterService(IAppDbContext context, ITelegramBotClient client, ILogger<RegisterService> logger, ISendMessageService sendMessageService)
        {
            _context = context;
            _client = client;
            _logger = logger;
            _sendMessageService = sendMessageService;
        }
        public static Domain.Entities.User? UserObject { get; set; }
        public async Task CatchMessageFromRegister(Message message, CancellationToken cancellationToken = default)
        {
            List<string>? languages = await _context.Languages.Select(x => x.Name).ToListAsync(cancellationToken);
            var state = Redis.GetUserState(message.Chat.Id);
            await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: $"Tilni tanglang!\nChoose language!\nВыберите язык!",
                    replyMarkup: ReplyKeyboards.MakingKeyboard(languages),
                    cancellationToken: cancellationToken);

            await Redis.SetUserState(message.Chat.Id, State.states[0]);// "language" state

            UserObject = new Domain.Entities.User
            {
                UserName = message.Chat.Username,
                TelegramId = message.Chat.Id
            };

            return;
        }

        public async Task SendContact(Domain.Entities.User user, Message message, CancellationToken cancellationToken = default)
        {
            if (user == null || message.Contact == null || message.Contact.PhoneNumber == null)
            {
                return;
            }

            user.Phone = message.Contact.PhoneNumber;

            await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: ReplyMessages.afterRegistered[user.LanguageId],
                    replyMarkup: ReplyKeyboards.MainPageKeyboards[user.LanguageId - 1],
                    cancellationToken: cancellationToken);

            await Redis.DeleteState(message.Chat.Id);

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await _sendMessageService.SendMessageToAdmins($"New User:\nName: {user.FirstName} {user.LastName}\nTelegramId: {user.TelegramId}\nCreatedTime: {user.CreatedTate.ToString("dd-mm-yyyy  hh-mm")}");

            UserObject = null;

            return;
        }

        public async Task SendFirstName(Domain.Entities.User user, Message message, CancellationToken cancellationToken = default)
        {
            user.FirstName = message.Text ?? $"{message.Chat.FirstName}";

            await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: ReplyMessages.askLastName[user.LanguageId],
                    cancellationToken: cancellationToken);

            await Redis.Next(message.Chat.Id);

            return;
        }

        public async Task SendLastName(Domain.Entities.User user, Message message, CancellationToken cancellationToken)
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

            await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: ReplyMessages.askContact[user.LanguageId],
                    replyMarkup: replyKeyboard,
                    cancellationToken: cancellationToken);

            await Redis.Next(message.Chat.Id);

            return;
        }

        public async Task SendLanguage(Domain.Entities.User user, Message message, CancellationToken cancellationToken)
        {
            try
            {
                if (user == null)
                {
                    return;
                }

                Language? language;
                language = await _context.Languages.FirstOrDefaultAsync(x => x.Name == message.Text!, cancellationToken);

                user.LanguageId = language?.Id ?? 1;

                await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: ReplyMessages.askFirstName[user.LanguageId],
                    replyMarkup: new ReplyKeyboardRemove(),
                    cancellationToken: cancellationToken);

                await Redis.Next(message.Chat.Id);

                return;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ERROR:  {ex}", ex.Message);
                return;
            }
        }
    }
}