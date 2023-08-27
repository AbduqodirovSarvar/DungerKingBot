using Dunger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Dunger.Application.Abstractions.TelegramBotAbstractions
{
    public interface IOrderServices
    {
        Task GetAllOrders(ITelegramBotClient _client, Message message, CancellationToken cancellationToken = default);
        Task ChangeOrderStatus(ITelegramBotClient _client, Message message, CancellationToken cancellationToken= default);
        List<string> SendingOrders(List<Order> orders, CancellationToken cancellationToken = default);
    }
}
