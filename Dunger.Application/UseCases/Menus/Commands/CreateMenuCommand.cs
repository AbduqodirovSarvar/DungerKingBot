using Dunger.Application.Abstractions;
using Dunger.Application.Models.ViewModels;

namespace Dunger.Application.UseCases.Menus.Commands
{
    public class CreateMenuCommand : ICommand<MenuViewModel>
    {
        public string Name { get; set; } = string.Empty;
        public string? PhotoPath { get; set; } = null;
        public string? Title { get; set; } = null;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
