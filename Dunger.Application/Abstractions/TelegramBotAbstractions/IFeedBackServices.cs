using Telegram.Bot.Types;

namespace Dunger.Application.Abstractions.TelegramBotAbstractions
{
    public interface IFeedBackServices
    {
        Task CreateFeedBack(Message message, CancellationToken cancellationToken);
    }
}
