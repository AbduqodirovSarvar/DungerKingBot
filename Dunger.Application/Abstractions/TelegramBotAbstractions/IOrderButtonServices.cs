using Dunger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Dunger.Application.Abstractions.TelegramBotAbstractions
{
    public interface IOrderButtonServices
    {
        Task GetPreviousOrders(long chatId, List<string> messages, int languageId, CancellationToken cancellationToken = default);
        Task GetNextOrders(long chatId, List<string> messages, int languageId, CancellationToken cancellationToken = default);
    }
}
