using Telegram.Bot.Types;

namespace Dunger.Application.Abstractions.TelegramBotAbstractions
{
    public interface IReceivedMessageService
    {
        Task CatchMessageWithState(Message message, string state, CancellationToken cancellationToken = default);
        Task CatchMessageWithoutState(Message message, CancellationToken cancellationToken = default);
        /*Task SendStartCommand(Message message, CancellationToken cancellationToken);

        Task SendHelpCommand(Message message, CancellationToken cancellationToken);

        Task HasStateCommand(string State, Message message, CancellationToken cancellationToken);
        Task ReceivedContactButton(Message message, CancellationToken cancellationToken);
        Task ReceivedInformationButton(Message message, CancellationToken cancellationToken);
        Task ReceivedMenuButton(Message message, CancellationToken cancellationToken);
        Task ReceivedOrdersButton(Message message, CancellationToken cancellationToken);
        Task ReceivedCommentsButton(Message message, CancellationToken cancellationToken);
        Task ReceivedSettingsButton(Message message, CancellationToken cancellationToken);
        Task UnknownCommand(Message message, CancellationToken cancellationToken);*/
    }
}
