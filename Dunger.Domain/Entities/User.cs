namespace Dunger.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public long TelegramId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public DateTime CreatedTate { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedTime { get; set; } = null;
    }
}
