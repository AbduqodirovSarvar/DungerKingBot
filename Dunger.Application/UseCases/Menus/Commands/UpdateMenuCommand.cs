using Dunger.Application.Abstractions;
using Dunger.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dunger.Application.UseCases.Menus.Commands
{
    public class UpdateMenuCommand : ICommand<MenuViewModel>
    {
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; } = null;
        public int? PhotoId { get; set; } = null;
        public string? Description { get; set; } = null;
        public decimal? Price { get; set; } = null;
        public int? LanguageId { get; set; } = null;
        public int? FilialId { get; set; } = null;
    }
}
