using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.Vehicles.Commands
{
    public class UpdateVehiclesCommand
    {
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; } = null;
    }
}
