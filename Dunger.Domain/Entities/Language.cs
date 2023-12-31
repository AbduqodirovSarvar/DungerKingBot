﻿namespace Dunger.Domain.Entities
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; } = DateTime.SpecifyKind(DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime, DateTimeKind.Utc).ToUniversalTime();
    }
}
