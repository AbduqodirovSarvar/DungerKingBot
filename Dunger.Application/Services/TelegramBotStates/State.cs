using Dunger.Application.Services.TelegramBotServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.Services.TelegramBotStates
{
    public class State
    {
        public static readonly string[] states = new[] 
        { 
            "language",// 0
            "firstName", // 1
            "lastName", // 2
            "contact", // 3
            "about", // 4
            "order", // 5
            "feedback" // 6
        };
    }
}
