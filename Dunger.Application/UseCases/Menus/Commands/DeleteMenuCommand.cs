using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.Menus.Commands
{
    public class DeleteMenuCommand
    {
        public DeleteMenuCommand(int MenuId) 
        {
            this.MenuId = MenuId;
        }
        [Required]
        public int MenuId { get; set; }
    }
}
