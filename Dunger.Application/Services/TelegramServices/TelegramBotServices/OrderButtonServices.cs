using Dunger.Application.Abstractions.TelegramBotAbstractions;
using Dunger.Application.Services.TelegramServices.TelegramBotKeyboards;
using Dunger.Application.Services.TelegramServices.TelegramBotMessages;
using Telegram.Bot;

namespace Dunger.Application.Services.TelegramServices.TelegramBotServices
{
    public class OrderButtonServices : IOrderButtonServices
    {
        private readonly ITelegramBotClient _client;
        public OrderButtonServices(ITelegramBotClient client)
        {
            _client = client;
        }

        private static int _currentIndex = 0;

        public static Task FinishedindexofOrder()
        {
            _currentIndex = 0;
            return Task.CompletedTask;
        }
        public async Task GetNextOrders(long chatId, List<string> messages, int languageId, CancellationToken cancellationToken = default)
        {
            List<string> msgs = messages.Skip(_currentIndex).Take(5).ToList();
            if (messages.Count == 0)
            {
                msgs = new() { ReplyMessages.emptyOrders[languageId] };
            }

            await _client.SendTextMessageAsync(chatId: chatId,
                    text: string.Join("", msgs),
                    replyMarkup: InlineKeyboards.OrderGetButtons[0],
                    cancellationToken: cancellationToken);

            _currentIndex += msgs.Count;

            return;

        }

        public async Task GetPreviousOrders(long chatId, List<string> messages, int languageId, CancellationToken cancellationToken = default)
        {
            if (_currentIndex < 5)
            {
                return;
            }
            if (messages.Skip(_currentIndex).Take(5).ToList().Count < 5 || _currentIndex < 5)
            {
                _currentIndex -= messages.Skip(_currentIndex).Take(5).ToList().Count;
            }
            else
            {
                _currentIndex -= 5;
            }

            List<string> msgs = messages.Skip(_currentIndex).Take(5).ToList();
            if (messages.Count == 0)
            {
                msgs = new() { ReplyMessages.emptyOrders[languageId] };
            }

            await _client.SendTextMessageAsync(chatId: chatId,
                    text: string.Join("", msgs),
                    replyMarkup: InlineKeyboards.OrderGetButtons[0],
                    cancellationToken: cancellationToken);

            return;

        }
    }
}
