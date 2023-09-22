using Dunger.Application.Abstractions;
using Dunger.Application.UseCases.Vehicles.Queries;
using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.Vehicles.QueryHandlers
{
    public class GetAllVehiclesQueryHandler : IQueryHandler<GetAllVehiclesQuery, List<Vehicle>>
    {
        private readonly IAppDbContext _context;
        public GetAllVehiclesQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Vehicle>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            List<Vehicle> vehicles = await _context.Vehicles.ToListAsync(cancellationToken);
            return vehicles;
        }
    }
}
