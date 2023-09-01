namespace Dunger.Application.UseCases.Menus.Commands
{
    public class CreateMenuCommand
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int LanguageId { get; set; }
        public int FilialId { get; set; }
    }
}
