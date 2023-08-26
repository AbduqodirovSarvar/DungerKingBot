namespace Dunger.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string Phone { get; set; } = string.Empty;
        public long TelegramId { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public DateTime CreatedTate { get; set; } = DateTime.SpecifyKind(DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime, DateTimeKind.Utc).ToUniversalTime();
        public DateTime? DeletedTime { get; set; } = null;
    }
}
