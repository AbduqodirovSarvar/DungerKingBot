﻿namespace Dunger.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public long TelegramId { get; set; }
        public User? User { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; } = DateTime.SpecifyKind(DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime, DateTimeKind.Utc).ToUniversalTime();
    }
}
