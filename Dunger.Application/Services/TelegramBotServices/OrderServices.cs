using Dunger.Application.Abstractions.TelegramBotAbstractions;
using Dunger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Dunger.Application.Services.TelegramBotServices
{
    public class OrderServices : IOrderServices
    {
        public Task ChangeOrderStatus(ITelegramBotClient _client, Message message, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task GetAllOrders(ITelegramBotClient _client, Message message, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public List<string> SendingOrders(List<Order> orders, CancellationToken cancellationToken = default)
        {
            List<string> result = new();
            StringBuilder msg = new();
            int number = 1;
            foreach (Order order in orders)
            {
                msg.AppendLine("1. Menyular:");
                foreach(var menu in order.Menus)
                {
                    msg.AppendLine($"Nomi: {menu.Menu!.Name}");
                    msg.AppendLine($"Narxi: {menu.Menu.Price}");
                    msg.AppendLine($"Soni: {menu.Amount}");
                    msg.AppendLine();
                }
                msg.AppendLine($"Buyurtmaning umumiy narxi: {order.TotalSumms}");
                msg.AppendLine($"Filial nomi: {order.Filial!.Name}");
                msg.AppendLine($"Yetkazilgan manzil: {order.Address}");
                msg.AppendLine($"Geolokatsiya: {order.LocationUrl ?? "Lokatsiya kiritilmagan"}");
                msg.AppendLine($"Yetkazilgan vaqti: {order.DeliveredTime!.Value.ToString("dd-MM-yyyy HH:mm")}");
                msg.AppendLine();msg.AppendLine();

                number++;

                if(number%5 == 0)
                {
                    result.Add(msg.ToString());
                    msg.Clear();
                }
            }
            if(msg.Length > 0)
            {
                result.Add(msg.ToString());
            }

            return result;
        }
    }
}
