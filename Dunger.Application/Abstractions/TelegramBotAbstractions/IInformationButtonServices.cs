using Telegram.Bot.Types;

namespace Dunger.Application.Abstractions.TelegramBotAbstractions
{
    public interface IInformationButtonServices
    {
        Task AboutTheFilial(CallbackQuery callbackQuery, CancellationToken cancellationToken = default);
        Task CatchMessageFromAbout(Message message, int langauageId, CancellationToken cancellationToken = default);
    }
}
