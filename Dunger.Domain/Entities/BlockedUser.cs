namespace Dunger.Domain.Entities
{
    public class BlockedUser
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public bool IsUnBlocked { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
