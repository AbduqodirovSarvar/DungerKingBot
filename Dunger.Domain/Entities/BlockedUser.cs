namespace Dunger.Domain.Entities
{
    public class BlockedUser
    {
        public int Id { get; set; }
        public long TelegramId { get; set; }
        public User? User { get; set; }
        public bool IsUnBlocked { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.SpecifyKind(DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime, DateTimeKind.Utc).ToUniversalTime();
    }
}
