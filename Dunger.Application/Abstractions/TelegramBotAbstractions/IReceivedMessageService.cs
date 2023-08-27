using Telegram.Bot;
using Telegram.Bot.Types;

namespace Dunger.Application.Abstractions.TelegramBotAbstractions
{
    public interface IReceivedMessageService
    {
        Task SendStartCommand(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);

        Task SendHelpCommand(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);

        Task HasStateCommand(ITelegramBotClient botClient, string State, Message message, CancellationToken cancellationToken);
        Task ReceivedContactButton(ITelegramBotClient botclient, Message message, CancellationToken cancellationToken);
        Task ReceivedInformationButton(ITelegramBotClient botclient, Message message, CancellationToken cancellationToken);
        Task ReceivedMenuButton(ITelegramBotClient botclient, Message message, CancellationToken cancellationToken);
        Task ReceivedOrdersButton(ITelegramBotClient botclient, Message message, CancellationToken cancellationToken);
        Task ReceivedCommentsButton(ITelegramBotClient botclient, Message message, CancellationToken cancellationToken);
        Task ReceivedSettingsButton(ITelegramBotClient botclient, Message message, CancellationToken cancellationToken);
        Task UnknownCommand(ITelegramBotClient botclient, Message message, CancellationToken cancellationToken);
    }
}
