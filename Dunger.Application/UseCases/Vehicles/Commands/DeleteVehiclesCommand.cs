using Dunger.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dunger.Application.UseCases.Vehicles.Commands
{
    public class DeleteVehiclesCommand : ICommand<bool>
    {
        public DeleteVehiclesCommand(int id) { Id = id; }
        [Required]
        public int Id { get; set; }
    }
}
