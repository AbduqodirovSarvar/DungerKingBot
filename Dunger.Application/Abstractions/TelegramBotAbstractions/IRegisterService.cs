using Telegram.Bot;
using Telegram.Bot.Types;

namespace Dunger.Application.Abstractions.TelegramBotAbstractions
{
    public interface IRegisterService
    {
        Task CatchMessage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);

        Task SendFirstName(ITelegramBotClient botClient, Domain.Entities.User user, Message message, CancellationToken cancellationToken);

        Task SendLastName(ITelegramBotClient botClient, Domain.Entities.User user, Message message, CancellationToken cancellationToken);

        Task SendContact(ITelegramBotClient botClient, Domain.Entities.User user, Message message, CancellationToken cancellationToken);

        Task SendLanguage(ITelegramBotClient botClient, Domain.Entities.User user, Message message, CancellationToken cancellationToken);

    }
}
