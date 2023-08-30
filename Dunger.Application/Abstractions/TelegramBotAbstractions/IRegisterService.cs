using Telegram.Bot;
using Telegram.Bot.Types;

namespace Dunger.Application.Abstractions.TelegramBotAbstractions
{
    public interface IRegisterService
    {
        Task CatchMessageFromRegister(Message message, CancellationToken cancellationToken);

        Task SendFirstName(Domain.Entities.User user, Message message, CancellationToken cancellationToken);

        Task SendLastName(Domain.Entities.User user, Message message, CancellationToken cancellationToken);

        Task SendContact(Domain.Entities.User user, Message message, CancellationToken cancellationToken);

        Task SendLanguage(Domain.Entities.User user, Message message, CancellationToken cancellationToken);

    }
}
