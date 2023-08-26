using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.Abstractions.TelegramBotAbstractions
{
    public interface ISendMessageService
    {
        Task SendMessageToAdmins(string message);
    }
}
