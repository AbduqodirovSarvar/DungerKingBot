
using Dunger.Application.Abstractions;
using Dunger.Application.Abstractions.TelegramBotAbstractions;
using Dunger.Application.Services.TelegramBotKeyboards;
using Dunger.Application.Services.TelegramBotMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly IOrderButtonServices _orderServices;
        private readonly IFeedBackServices _feedBackServices;
        private readonly ITelegramBotClient _client;
        private readonly IInformationButtonServices _infoServices;
        private readonly ILogger<ReceivedMessageService> _logger;
        public ReceivedMessageService(IAppDbContext context, IRegisterService registerService, IInformationButtonServices informationButtonServices,
            Redis redis, IOrderButtonServices orderServices, IFeedBackServices feedBackServices, ITelegramBotClient client, ILogger<ReceivedMessageService> logger)
        {
            _context = context;
            _registerService = registerService;
            _redis = redis;
            _orderServices = orderServices;
            _feedBackServices = feedBackServices;
            _client = client;
            _infoServices = informationButtonServices;
            _logger = logger;
        }

        public async Task CatchMessageWithState(Message message, string state, CancellationToken cancellationToken = default)
        {
            try
            {
                if (message.Text == "/start")
                {
                    await SendStartCommand(message, cancellationToken);
                    return;
                }

                if (message.Text == "/help")
                {
                    await SendHelpCommand(message, cancellationToken);
                    return;
                }

                Domain.Entities.User? user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == message.Chat.Id, cancellationToken);

                if (user == null)
                {
                    Task StateWHenUserNull = state switch
                    {
                        "language" => _registerService.SendLanguage(RegisterService.UserObject!, message, cancellationToken),
                        "firstName" => _registerService.SendFirstName(RegisterService.UserObject!, message, cancellationToken),
                        "lastName" => _registerService.SendLastName(RegisterService.UserObject!, message, cancellationToken),
                        "contact" => _registerService.SendContact(RegisterService.UserObject!, message, cancellationToken),
                        _ => SendStartCommand(message, cancellationToken)
                    };

                    await StateWHenUserNull;

                    return;
                }

                Task StateWhenUserNotNull = state switch
                {
                    "feedback" => _feedBackServices.CreateFeedBack(message, cancellationToken),
                    "about" => _infoServices.CatchMessageFromAbout(message, user.LanguageId, cancellationToken),
                    _ => SendStartCommand(message, cancellationToken)
                };

                await StateWhenUserNotNull;

                return;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ERROR: {ex.Message}", ex.Message);
                return;
            }

        }

        public async Task CatchMessageWithoutState(Message message, CancellationToken cancellationToken = default)
        {
            try
            {
                if (message.Text == "/start")
                {
                    await SendStartCommand(message, cancellationToken);
                    return;
                }

                if (message.Text == "/help")
                {
                    await SendHelpCommand(message, cancellationToken);
                    return;
                }

                Domain.Entities.User? user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == message.Chat.Id, cancellationToken);

                if (user == null)
                {
                    await SendStartCommand(message, cancellationToken);
                    return;
                }

                Task msg = message.Text switch
                {
                    "/start" => SendStartCommand(message, cancellationToken),
                    "/help" => SendHelpCommand(message, cancellationToken),
                    "Contact" or "Aloqa" or "Контакт" => ReceivedContactButton(message, cancellationToken),
                    "Biz haqimizda" or "About Us" or "О нас" => ReceivedInformationButton(message, cancellationToken),
                    "Menyu" or "Menu" or "Меню" => ReceivedMenuButton(message, cancellationToken),
                    "Buyurtmalarim" or "My Orders" or "Мои заказы" => ReceivedOrdersButton(message, cancellationToken),
                    "Fikr bildirish" or "Feedback" or "Обратная связь" => ReceivedFeedbackButton(message, cancellationToken),
                    "Sozlamalar" or "Settings" or "Настройки" => ReceivedSettingsButton(message, cancellationToken),
                    _ => UnknownCommand(message, cancellationToken)
                };

                await msg;

                return;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ERROR: {ex.Message}", ex.Message);
                return;
            }
        }
        private async Task SendStartCommand(Message message, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == message.Chat.Id, cancellationToken);

                if (user == null)
                {
                    await _registerService.CatchMessageFromRegister(message, cancellationToken);
                    return;
                }

                await OrderButtonServices.FinishedindexofOrder();

                await _redis.DeleteState(user.TelegramId);

                await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: ReplyMessages.chooseCommand[user.LanguageId],
                    replyMarkup: ReplyKeyboards.MainPageKeyboards[user.LanguageId - 1],
                    cancellationToken: cancellationToken);

                return;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ERROR: {ex.Message}", ex.Message);
                return;
            }
        }
        private async Task SendHelpCommand(Message message, CancellationToken cancellationToken)
        {
            try
            {
                Domain.Entities.User? user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == message.Chat.Id, cancellationToken);

                string msg = "Assalomu aleykum\nBotimizdan foydalanish uchun /start buyrug'ini bosing!";

                if (user != null)
                {
                    msg = ReplyMessages.helpMessages[user.LanguageId];
                }

                await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: msg,
                    cancellationToken: cancellationToken);

                await _redis.DeleteState(message.Chat.Id);

                await OrderButtonServices.FinishedindexofOrder();

                return;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ERROR: {ex.Message}", ex.Message);
                return;
            }
        }

        private async Task ReceivedContactButton(Message message, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == message.Chat.Id, cancellationToken);
                if (user == null)
                {
                    await _client.SendTextMessageAsync(message.Chat.Id,
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

                await _client.SendTextMessageAsync(chatId: message.Chat.Id, text: msg, cancellationToken: cancellationToken);

                return;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ERROR: {ex.Message}", ex.Message);
                return;
            }
        }

        private async Task ReceivedInformationButton(Message message, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == message.Chat.Id, cancellationToken);
                if (user == null)
                {
                    await _client.SendTextMessageAsync(message.Chat.Id,
                        $"{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}",
                        cancellationToken: cancellationToken);

                    await _redis.DeleteState(message.Chat.Id);

                    return;
                }

                await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: ReplyMessages.chooseCommand[user.LanguageId],
                    replyMarkup: ReplyKeyboards.AboutPageKeyboards[user.LanguageId - 1],
                    cancellationToken: cancellationToken);

                await _redis.SetUserState(user.TelegramId, "about");

                return;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ERROR: {ex.Message}", ex.Message);
                return;
            }
        }

        private async Task ReceivedMenuButton(Message message, CancellationToken cancellationToken)
        {
            var chatId = message.Chat.Id;
            await _client.SendTextMessageAsync(chatId, "Opening the website...");
            await _client.SendChatActionAsync(chatId, Telegram.Bot.Types.Enums.ChatAction.Typing);

            // Simulate delay before opening the website
            await System.Threading.Tasks.Task.Delay(2000);

            // Open the website link
            await _client.SendTextMessageAsync(chatId, "Website: https://lms.tuit.uz/auth/login");
            //await _client.SendTextMessageAsync(message.Chat.Id, "Siz Menu tugmasini bosdingiz. Bu brauzer oynasida ochiladi!", cancellationToken: cancellationToken);
            return;
        }

        private async Task ReceivedOrdersButton(Message message, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == message.Chat.Id, cancellationToken);
                if (user == null)
                {
                    await _client.SendTextMessageAsync(message.Chat.Id,
                        $"{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}",
                        cancellationToken: cancellationToken);
                    return;
                }

                var orders = await _context.Orders.Include(x => x.Menus).ThenInclude(x => x.Menu).Include(x => x.Filial).Where(x => x.TelegramId == user.TelegramId).OrderByDescending(x => x.Id).ToListAsync(cancellationToken);

                List<string> messages = ReplyMessages.OrderMessages(orders, user.LanguageId, cancellationToken);

                await OrderButtonServices.FinishedindexofOrder();

                await _orderServices.GetNextOrders(message.Chat.Id, messages, user.LanguageId, cancellationToken);

                return;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ERROR: {ex.Message}", ex.Message);
                return;
            }
        }

        private async Task ReceivedFeedbackButton(Message message, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == message.Chat.Id, cancellationToken);
                if (user == null)
                {
                    await _client.SendTextMessageAsync(message.Chat.Id,
                        $"{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}",
                        cancellationToken: cancellationToken);

                    await _registerService.CatchMessageFromRegister(message, cancellationToken);
                    return;
                }

                await _client.SendTextMessageAsync(message.Chat.Id,
                    ReplyMessages.askFeedback[user.LanguageId],
                    replyMarkup: new ReplyKeyboardRemove(),
                    cancellationToken: cancellationToken);

                await _redis.SetUserState(message.Chat.Id, "feedback");
                return;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ERROR: {ex.Message}", ex.Message);
                return;
            }
        }

        private async Task ReceivedSettingsButton(Message message, CancellationToken cancellationToken)
        {
            await _client.SendTextMessageAsync(message.Chat.Id, "Siz Settings bolimini ochdingiz, bu bo;lim brauzer oynasida sozlanadi", cancellationToken: cancellationToken);
            return;
        }

        private async Task UnknownCommand(Message message, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == message.Chat.Id, cancellationToken);
                if (user == null)
                {
                    await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                        text: $"{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}",
                        cancellationToken: cancellationToken);
                    return;
                }

                await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: ReplyMessages.unKnownCommand[user.LanguageId],
                    cancellationToken: cancellationToken);

                return;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ERROR: {ex.Message}", ex.Message);
                return;
            }
        }

    }
}
