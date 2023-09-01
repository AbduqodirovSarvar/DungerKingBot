namespace Dunger.Domain.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int? PhotoId { get; set; }
        public MenuPhoto? Photo { get; set; }
    }
}
