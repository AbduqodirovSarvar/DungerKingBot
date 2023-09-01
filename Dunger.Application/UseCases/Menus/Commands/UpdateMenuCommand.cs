using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.Menus.Commands
{
    public class UpdateMenuCommand
    {
        public string? Name { get; set; } = null;
        public string? PhotoPath { get; set; } = null;
        public string? Title { get; set; } = null;
        public string? Description { get; set; } = null;
        public decimal? Price { get; set; } = null;
        public int? LanguageId { get; set; } = null;
        public int? FilialId { get; set; } = null;
    }
}
