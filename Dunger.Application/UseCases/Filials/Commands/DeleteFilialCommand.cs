using Dunger.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.Filials.Commands
{
    public class DeleteFilialCommand : ICommand<bool>
    {
        public DeleteFilialCommand(int id) { Id = id; }
        [Required]
        public int Id { get; set; }
    }
}
