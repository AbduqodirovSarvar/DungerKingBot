using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Dunger.Application.Abstractions.TelegramBotAbstractions
{
    public interface IInformationButtonServices
    {
        Task AboutTheFilial(CallbackQuery callbackQuery, CancellationToken cancellationToken = default);
        Task CatchMessageFromAbout(Message message, int langauageId, CancellationToken cancellationToken = default);
    }
}
