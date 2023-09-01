using Telegram.Bot.Types;

namespace Dunger.Application.Abstractions.TelegramBotAbstractions
{
    public interface IReceivedCallbackQueryServices
    {
        Task CatchCallbackQueryWithState(CallbackQuery callbackQuery, string state, CancellationToken cancellationToken = default);
        Task CatchCallbackQueryWithoutState(CallbackQuery callbackQuery, CancellationToken cancellationToken = default);
    }
}
