using Dunger.Domain.Enums;

namespace Dunger.Domain.Entities
{
    public class Deliver
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly BirthDay { get; set; }
        public Gender Gender { get; set; } = Gender.Male;
        public string Phone { get; set; } = string.Empty;
        public int VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }
        public string VehicleColor { get; set; } = "unknown";
        public string VehicleNumber { get; set; } = string.Empty;
        public int PhotoId { get; set; }
        public DeliverPhoto? DeliverPhoto { get; set; }
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public DateTime JoinedTime { get; set; } = DateTime.SpecifyKind(DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime, DateTimeKind.Utc).ToUniversalTime();
    }
}
