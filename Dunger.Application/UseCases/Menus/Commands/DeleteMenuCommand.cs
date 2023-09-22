using Dunger.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dunger.Application.UseCases.Menus.Commands
{
    public class DeleteMenuCommand : ICommand<bool>
    {
        public DeleteMenuCommand(int MenuId) 
        {
            this.MenuId = MenuId;
        }
        [Required]
        public int MenuId { get; set; }
    }
}
