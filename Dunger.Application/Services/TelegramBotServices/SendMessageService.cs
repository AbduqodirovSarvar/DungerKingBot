using Dunger.Application.Abstractions.TelegramBotAbstractions;
using Dunger.Application.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Dunger.Application.Services.TelegramBotServices
{
    public class SendMessageService : ISendMessageService
    {
        private readonly ITelegramBotClient _client;
        private readonly BotConfiguration _botConfiguration;
        public SendMessageService(ITelegramBotClient client, IOptions<BotConfiguration> options)
        {
            _client = client;
            _botConfiguration = options.Value;
        }

        public async Task SendMessageToAdmins(string message)
        {
            List<long> adminIds = _botConfiguration.AdminTelegramIds.Split(",").Select(x => long.Parse(x)).ToList();

            foreach (var Id in adminIds)
            {
                try
                {
                    await _client.SendTextMessageAsync(chatId: Id, text: message);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
        }
    }
}
