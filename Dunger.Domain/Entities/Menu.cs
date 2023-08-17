namespace Dunger.Domain.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
        public int FilialId { get; set; }
        public Filial? Filial { get; set; }
        public ICollection<MenuPhoto> Photos { get; set; } = new HashSet<MenuPhoto>();
    }
}
