using Dunger.Application.Abstractions;
using Dunger.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dunger.Application.UseCases.Filials.Commands
{
    public class UpdateFilialCommand : ICommand<FilialViewModel>
    {
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
        public string? Address { get; set; } = null;
        public string? LocationUrl { get; set; } = null;
    }
}
}
