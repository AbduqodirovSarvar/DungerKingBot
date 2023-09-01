using Dunger.Application.Abstractions;
using Dunger.Application.Models.ViewModels;
using Dunger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.Filials.Commands
{
    public class FilialCreateCommand : ICommand<FilialViewModel>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string LocationUrl { get; set; } = string.Empty;
        public int LanguageId { get; set; }
    }
}
