using Dunger.Application.Abstractions;
using Dunger.Application.Abstractions.TelegramBotAbstractions;
using Dunger.Application.Services.TelegramBotKeyboards;
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

                //await SendLanguage(botClient, UserObject, message, cancellationToken);
                return;
            }

            else if (state == RegisterState.States[0] && UserObject != null)
            {
               // await SendFirstName(botClient, UserObject, message, cancellationToken);
            }
            else if (state == RegisterState.States[1] && UserObject != null)
            {
               // await SendLastName(botClient, UserObject, message, cancellationToken);
            }
            else if (state == RegisterState.States[2] && UserObject != null)
            {
              //  await SendContact(botClient, UserObject, message, cancellationToken);
            }
        }

        public async Task SendContact(ITelegramBotClient botClient, Domain.Entities.User user, Message message, CancellationToken cancellationToken)
        {
            if(message.Contact?.PhoneNumber == null)
            {
                return;
            }
            user.Phone = message.Contact.PhoneNumber;
            string msg = user.LanguageId switch
            {
                1 => "Tabriklaymiz!\nSiz r'yhatdan muvaffaqiyatli o'tdingiz!\nKerakli bo'limni tanlang!",
                2 => "Congratulations!\nYou have successfully passed the exam!\nChoose the required section!",
                3 => "Поздравляем!\nВы успешно сдали экзамен!\nВыберите необходимый раздел!",
                _ => "Tabriklaymiz!\nSiz r'yhatdan muvaffaqiyatli o'tdingiz!\nKerakli bo'limni tanlang!"
            };

            await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: msg,
                    replyMarkup: ReplyKeyboards.BotOnReceivedStart,
                    cancellationToken: cancellationToken);

            await _state.Finished(message.Chat.Id);

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            UserObject = null;

            return;
        }

        public async Task SendFirstName(ITelegramBotClient botClient, Domain.Entities.User user, Message message, CancellationToken cancellationToken)
        {
            if (message.Text == null)
            {
                return;
            }
            user.FirstName = message.Text ?? $"{message.Chat.FirstName}";
            string msg = user.LanguageId switch
            {
                1 => $"Familiyangizni kiriting:",
                2 => $"Enter your last name:",
                3 => $"Введите свою фамилию:",
                _ => $"Familiyangizni kiriting:"
            };

            await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: msg,
                    cancellationToken: cancellationToken);

            await _state.Next(message.Chat.Id);

            return;
        }

        public async Task SendLastName(ITelegramBotClient botClient, Domain.Entities.User user, Message message, CancellationToken cancellationToken)
        {
            user.LastName = message.Text ?? $"{message.Chat.LastName}";
            string msg = user.LanguageId switch
            {
                1 => $"Kantaktingizni yuboring:",
                2 => $"Send your contact:",
                3 => $"Отправьте свой контакт:",
                _ => $"Kantaktingizni yuboring:"
            };

            string replyMarkupText = user.LanguageId switch
            {
                1 => "Kontaktni ulashish",
                2 => "Share Contact",
                3 => "Поделиться контактом",
                _ => "Kontaktni ulashish"
            };

            ReplyKeyboardMarkup replyKeyboard = new ReplyKeyboardMarkup(
                new[]
                {
                    new KeyboardButton[] { new KeyboardButton(replyMarkupText)
                    { RequestContact = true } }
                });

            replyKeyboard.ResizeKeyboard = true;

            await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: msg,
                    replyMarkup: replyKeyboard,
                    cancellationToken: cancellationToken);

            await _state.Next(message.Chat.Id);

            return;
        }

        public async Task SendLanguage(ITelegramBotClient botClient, Domain.Entities.User user, Message message, CancellationToken cancellationToken)
        {
            if (message.Text == null)
            {
                return;
            }
            Language? lang = await _context.Languages.FirstOrDefaultAsync(x => x.Name == message.Text.ToString(), cancellationToken);
            user.LanguageId = lang?.Id ?? 1;


            string msg = user.LanguageId switch
            {
                1 => $"Assalomu aleykum\nBotimizga hush kelibsiz!\n\nBotdan foydalanish uchun ro'yxatdan o'ting!\n\nIsmingizni kiriting:",
                2 => $"Hello\nWelcome to our bot!\n\nRegister to use the bot!\n\nEnter your first name:",
                3 => $"Привет\nДобро пожаловать к нашему боту!\n\nЗарегистрируйтесь, чтобы использовать бот!\n\nВведите свое имя:",
                _ => $"Assalomu aleykum\nBotimizga hush kelibsiz!\n\nBotdan foydalanish uchun ro'yxatdan o'ting!\n\nIsmingizni kiriting:"
            };

            await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: msg,
                    replyMarkup: new ReplyKeyboardRemove(),
                    cancellationToken: cancellationToken);
            await _state.Next(message.Chat.Id);

            return;
        }
    }
}
