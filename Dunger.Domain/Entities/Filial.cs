namespace Dunger.Domain.Entities
{
    public class Filial
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string LocationUrl { get; set; } = string.Empty;
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public ICollection<Menu> Menus { get; set; } = new HashSet<Menu>();
        public DateTime CreatedTime { get; set; } = DateTime.SpecifyKind(DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(5)).DateTime, DateTimeKind.Utc).ToUniversalTime();
    }
}