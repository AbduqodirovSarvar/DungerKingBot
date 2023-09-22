using Dunger.Application.Abstractions;
using Dunger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dunger.Application.UseCases.Menus.Commands
{
    public class CreateMenuPhotoCommand : ICommand<MenuPhoto>
    {
        public int MenuId { get; set; }
        public string PhotoPath { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
