using Dunger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Dunger.Application.Abstractions.TelegramBotAbstractions
{
    public interface IReceivedCallbackQueryServices
    {
        Task CatchCallbackQueryWithState(CallbackQuery callbackQuery, string state, CancellationToken cancellationToken = default);
        Task CatchCallbackQueryWithoutState(CallbackQuery callbackQuery, CancellationToken cancellationToken = default);
    }
}
