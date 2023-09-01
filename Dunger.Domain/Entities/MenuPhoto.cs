namespace Dunger.Domain.Entities
{
    public class MenuPhoto
    {
        public int Id { get; set; }
        public int? MenuId { get; set; }
        public Menu? Menu { get; set; }
        public string PhotoPath { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
