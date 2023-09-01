using Dunger.Domain.Entities;

namespace Dunger.Application.Models.ViewModels
{
    public class FilialViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string LocationUrl { get; set; } = string.Empty;
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Menu>? Menus { get; set; }
        public ICollection<string> Photos { get; set; } = new HashSet<string>();
        public DateTime CreatedTime { get; set; }
    }
}
