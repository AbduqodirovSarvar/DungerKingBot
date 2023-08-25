using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.Models
{
    public class BotConfiguration
    {
        public string Token { get; set; } = string.Empty;
        public string HostAddress { get; set; } = string.Empty;
        public string UserIds { get; set; } = string.Empty;
    }
}