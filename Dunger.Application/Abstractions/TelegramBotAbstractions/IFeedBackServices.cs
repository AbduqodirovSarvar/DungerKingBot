using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace Dunger.Application.Abstractions.TelegramBotAbstractions
{
    public interface IFeedBackServices
    {
        Task CreateFeedBack(ITelegramBotClient botclient, Message message, CancellationToken cancellationToken);
    }
}
