using Dunger.Domain.Entities;

namespace Dunger.Application.Models.ViewModels
{
    public class MenuViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int LanguageId { get; set; }
        public int FilialId { get; set; }
        public ICollection<string> Photos { get; set; } = new HashSet<string>();
    }
}
