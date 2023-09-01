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
    public class ReceivedCallbackQueryServices : IReceivedCallbackQueryServices
    {
        private readonly IOrderButtonServices _orderServices;
        private readonly IAppDbContext _context;
        private readonly ITelegramBotClient _client;
        private readonly IInformationButtonServices _informationButtonServices;
        private readonly ILogger<ReceivedCallbackQueryServices> _logger;
        public ReceivedCallbackQueryServices(IOrderButtonServices orderServices, IAppDbContext context,
            ITelegramBotClient client, IInformationButtonServices informationButtonServices, ILogger<ReceivedCallbackQueryServices> logger)
        {
            _orderServices = orderServices;
            _context = context;
            _client = client;
            _informationButtonServices = informationButtonServices;
            _logger = logger;
        }

        public async Task CatchCallbackQueryWithState(CallbackQuery callbackQuery, string state, CancellationToken cancellationToken = default)
        {
            try
            {
                Domain.Entities.User? user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == callbackQuery.From.Id, cancellationToken);

                if (user == null || callbackQuery.Data == null)
                {
                    await _client.SendTextMessageAsync(callbackQuery.From.Id,
                            $"{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}",
                            replyMarkup: new ReplyKeyboardRemove(),
                            cancellationToken: cancellationToken);

                    await OrderButtonServices.FinishedindexofOrder();

                    return;
                }

                List<Domain.Entities.Order> orders = await _context.Orders.ToListAsync(cancellationToken);

                List<string> orderStringList = ReplyMessages.OrderMessages(orders, user.LanguageId, cancellationToken);

                List<string> filials = await _context.Filials.Select(x => x.Name.ToLower()).ToListAsync(cancellationToken);

                Task State = state switch
                {
                    "about" when filials.Contains(callbackQuery.Data) => _informationButtonServices.AboutTheFilial(callbackQuery, cancellationToken),
                    "order" when new[] { "Keyingis", "Next", "Следующий" }.Any(x => x == callbackQuery.Data) => _orderServices.GetNextOrders(callbackQuery.From.Id, orderStringList, user.LanguageId, cancellationToken),
                    "order" when new[] { "Avvalgisi", "Previous", "Предыдущий" }.Any(x => x == callbackQuery.Data) => _orderServices.GetPreviousOrders(callbackQuery.From.Id, orderStringList, user.LanguageId, cancellationToken),
                    _ => ToMainMenu(callbackQuery, user, cancellationToken)
                };

                await State;

                return;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ERROR: {ex.Message}", ex.Message);
                return;
            }
        }

        public async Task CatchCallbackQueryWithoutState(CallbackQuery callbackQuery, CancellationToken cancellationToken = default)
        {
            try
            {
                Domain.Entities.User? user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == callbackQuery.From.Id, cancellationToken);

                if (user == null || callbackQuery.Data == null)
                {
                    await _client.SendTextMessageAsync(callbackQuery.From.Id,
                            $"{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}",
                            replyMarkup: new ReplyKeyboardRemove(),
                            cancellationToken: cancellationToken);

                    await OrderButtonServices.FinishedindexofOrder();

                    return;
                }

                List<string> filials = await _context.Filials.Select(x => x.Name.ToLower()).ToListAsync(cancellationToken);

                Task callback = callbackQuery.Data switch
                {
                    _ when filials.Contains(callbackQuery.Data) => _informationButtonServices.AboutTheFilial(callbackQuery, cancellationToken),
                    _ => ToMainMenu(callbackQuery, user, cancellationToken)
                };

                await callback;

                return;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ERROR: {ex.Message}", ex.Message);
                return;
            }
        }

        private async Task ToMainMenu(CallbackQuery callbackQuery, Domain.Entities.User user, CancellationToken cancellationToken = default)
        {
            try
            {
                await _client.SendTextMessageAsync(chatId: callbackQuery.From.Id,
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
    }
}
