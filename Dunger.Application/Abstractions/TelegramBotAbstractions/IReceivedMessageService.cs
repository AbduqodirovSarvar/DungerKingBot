using Telegram.Bot;
using Telegram.Bot.Types;

namespace Dunger.Application.Abstractions.TelegramBotAbstractions
{
    public interface IReceivedMessageService
    {
        public Task SendStartCommand(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);

        public Task SendHelpCommand(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);

        public Task Usage(ITelegramBotClient botClient, string State, Message message, CancellationToken cancellationToken);
    }
}
