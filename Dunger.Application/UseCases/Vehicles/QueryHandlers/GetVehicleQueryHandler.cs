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
    public class GetVehicleQueryHandler : IQueryHandler<GetVehicleQuery, Vehicle>
    {
        private readonly IAppDbContext _context;
        public GetVehicleQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
        {
            Vehicle? vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (vehicle == null)
            {
                throw new Exception("Vehicle not found");
            }
            return vehicle;
        }
    }
}
