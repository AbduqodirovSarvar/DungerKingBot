using Dunger.Application.Abstractions;
using Dunger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.Vehicles.Queries
{
    public class GetVehicleQuery : IQuery<Vehicle>
    {
        public GetVehicleQuery(int id ) { Id = id; }
        [Required]
        public int Id { get; set; }
    }
}
