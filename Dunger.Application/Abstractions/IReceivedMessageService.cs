using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Dunger.Application.Abstractions
{
    public interface IReceivedMessageService
    {
        public Task<Message> SendStartCommand(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);
        public Task<Message> SendHelpCommand(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);

        public Task<Message> StartInlineQuery(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);

        public Task<Message> RequestContactAndLocation(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);

        public Task<Message> SendFile(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);

        public Task<Message> RemoveKeyboard(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);

        public Task<Message> SendReplyKeyboard(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);

        public Task<Message> SendInlineKeyboard(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);
    }
}
