using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Domain.Entities
{
    public class DeliverPhoto
    {
        public int Id { get; set; }
        public int DeliverId { get; set; }
        public Deliver? Deliver { get; set; }
        public string PhotoPath { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; } = DateTime.SpecifyKind(DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime, DateTimeKind.Utc).ToUniversalTime();
    }
}
