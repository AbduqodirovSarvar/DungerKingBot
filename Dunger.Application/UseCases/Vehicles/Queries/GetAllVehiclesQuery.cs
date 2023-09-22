using Dunger.Application.Abstractions;
using Dunger.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.Vehicles.Queries
{
    public class GetAllVehiclesQuery : IQuery<List<Vehicle>>
    {
        public GetAllVehiclesQuery() { }
    }
}
