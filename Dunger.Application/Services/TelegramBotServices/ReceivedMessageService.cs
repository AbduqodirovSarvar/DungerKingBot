
using Dunger.Application.Abstractions;
using Dunger.Application.Abstractions.TelegramBotAbstractions;
using Dunger.Application.Services.TelegramBotKeyboards;
using Dunger.Application.Services.TelegramBotMessages;
using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Dunger.Application.Services.TelegramBotServices
{
    public class ReceivedMessageService : IReceivedMessageService
    {
        private readonly IAppDbContext _context;
        private readonly IRegisterService _registerService;
        private readonly Redis _redis;
        private readonly IOrderServices _orderServices;
        private readonly IFeedBackServices _feedBackServices;
        public ReceivedMessageService(IAppDbContext context, IRegisterService registerService,
            Redis redis, IOrderServices orderServices, IFeedBackServices feedBackServices)
        {
            _context = context;
            _registerService = registerService;
            _redis = redis;
            _orderServices = orderServices;
            _feedBackServices = feedBackServices;
        }
        public async Task SendStartCommand(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == message.Chat.Id, cancellationToken);

                if (user == null)
                {
                    await _registerService.CatchMessage(botClient, message, cancellationToken);
                    return;
                }
                
                await _redis.DeleteState(user.TelegramId);

                var text = user.LanguageId switch
                {
                    1 => $"Assalomu aleykum {user.FirstName}\nBotimizga hush kelibsiz!",
                    2 => $"Hello {user.FirstName}\nWelcome to our bot!",
                    3 => $"Привет {user.FirstName}\nДобро пожаловать к нашему боту!",
                    _ => $"Assalomu aleykum {user.FirstName}\nBotimizga hush kelibsiz!"
                };

                await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: text,
                    replyMarkup: ReplyKeyboards.MainPageKeyboards[user.LanguageId - 1],
                    cancellationToken: cancellationToken);

                return;
            }
            catch (Exception)
            {
                return;
            }
        }
        public async Task SendHelpCommand(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            await botClient.SendTextMessageAsync(chatId: message.Chat.Id, text: "Assalomu aleykum\nBotimizdan foydalanish uchun /start buyrug'ini bosing!", cancellationToken: cancellationToken);

            await _redis.DeleteState(message.Chat.Id);
            
            return;
        }

        public async Task HasStateCommand(ITelegramBotClient botClient, string State, Message message, CancellationToken cancellationToken)
        {
            var msg = message.Text switch
            {
                "/start" => _redis.DeleteState(message.Chat.Id),
                "/help" => SendHelpCommand(botClient, message, cancellationToken),
                _ => DontWork(),
            };

            await msg;

            var state = State switch
            {
                "Language" => _registerService.SendLanguage(botClient, RegisterService.UserObject!, message, cancellationToken),
                "FirstName" => _registerService.SendFirstName(botClient, RegisterService.UserObject!, message, cancellationToken),
                "LastName" => _registerService.SendLastName(botClient, RegisterService.UserObject!, message, cancellationToken),
                "Contact" => _registerService.SendContact(botClient, RegisterService.UserObject!, message, cancellationToken),
                "Feedback" => _feedBackServices.CreateFeedBack(botClient, message, cancellationToken),
                _ => _registerService.CatchMessage(botClient, message, cancellationToken)
            };

            await state;

            return;
        }

        

        public async Task ReceivedContactButton(ITelegramBotClient botclient, Message message, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == message.Chat.Id, cancellationToken);
            if (user == null)
            {
                await botclient.SendTextMessageAsync(message.Chat.Id,
                    $"{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}",
                    cancellationToken: cancellationToken);
                return;
            }
            int langId = user.LanguageId;

            string msg = langId switch
            {
                1 => "Telefon raqami: +998932340316",
                2 => "Phone number: +998932340316",
                3 => "Номер телефона: +998932340316",
                _ => "Telefon raqami: +998932340316"
            };

            await botclient.SendTextMessageAsync(chatId: message.Chat.Id, text: msg, cancellationToken: cancellationToken);

            return;
        }

        public async Task ReceivedInformationButton(ITelegramBotClient botclient, Message message, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == message.Chat.Id, cancellationToken);
            if (user == null)
            {
                await botclient.SendTextMessageAsync(message.Chat.Id,
                    $"{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}",
                    cancellationToken: cancellationToken);
                return;
            }

            await botclient.SendTextMessageAsync(chatId: message.Chat.Id,
                text: ReplyMessages.chooseCommand[user.LanguageId],
                replyMarkup: ReplyKeyboards.AboutPageKeyboards[user.LanguageId - 1],
                cancellationToken: cancellationToken);

            await _redis.SetUserState(user.TelegramId, "About");
            return;
        }

        public async Task ReceivedMenuButton(ITelegramBotClient botclient, Message message, CancellationToken cancellationToken)
        {
            Console.WriteLine("Menu buttob bosildi");
            await botclient.SendTextMessageAsync(message.Chat.Id, "Siz Menu tugmasini bosdingiz. Bu brauzer oynasida ochiladi!", cancellationToken: cancellationToken);
            return;
        }

        public async Task ReceivedOrdersButton(ITelegramBotClient botclient, Message message, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == message.Chat.Id, cancellationToken);
                if (user == null)
                {
                    await botclient.SendTextMessageAsync(message.Chat.Id,
                        $"{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}",
                        cancellationToken: cancellationToken);
                    return;
                }

                var orders = await _context.Orders.Include(x => x.Menus).ThenInclude(x => x.Menu).Include(x => x.Filial).Where(x => x.TelegramId == user.TelegramId).OrderByDescending(x => x.Id).ToListAsync(cancellationToken);

                List<string> messages = _orderServices.SendingOrders(orders, cancellationToken);

                foreach (string msg in messages)
                {
                    await botclient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: msg,
                    cancellationToken: cancellationToken);
                }

                return;
            }
            catch (Exception)
            {
                return;
            }
        }

        public async Task ReceivedCommentsButton(ITelegramBotClient botclient, Message message, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == message.Chat.Id, cancellationToken);
            if (user == null)
            {
                await botclient.SendTextMessageAsync(message.Chat.Id,
                    $"{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}",
                    cancellationToken: cancellationToken);
                return;
            }

            await botclient.SendTextMessageAsync(message.Chat.Id,
                ReplyMessages.askFeedback[user.LanguageId],
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);

            await _redis.SetUserState(message.Chat.Id, "Feedback");
            return;
        }

        public async Task ReceivedSettingsButton(ITelegramBotClient botclient, Message message, CancellationToken cancellationToken)
        {
            await botclient.SendTextMessageAsync(message.Chat.Id, "Siz Settings bolimini ochdingiz, bu bo;lim brauzer oynasida sozlanadi", cancellationToken: cancellationToken);
            return;
        }

        public async Task UnknownCommand(ITelegramBotClient botclient, Message message, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == message.Chat.Id, cancellationToken);
            if (user == null)
            {
                await botclient.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: $"{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}",
                    cancellationToken: cancellationToken);
                return;
            }

            await botclient.SendTextMessageAsync(chatId: message.Chat.Id,
                text: ReplyMessages.unKnownCommand[user.LanguageId],
                cancellationToken: cancellationToken);

            return;
        }
        private static Task DontWork()
        {
            return Task.CompletedTask;
        }
    }
}
