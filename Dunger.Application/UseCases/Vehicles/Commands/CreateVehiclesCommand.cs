using Dunger.Application.Abstractions;
using Dunger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dunger.Application.UseCases.Vehicles.Commands
{
    public class CreateVehiclesCommand : ICommand<Vehicle>
    {
        public CreateVehiclesCommand(string name ) { Name = name; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
