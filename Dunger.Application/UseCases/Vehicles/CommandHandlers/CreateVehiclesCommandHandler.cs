using Dunger.Application.Abstractions;
using Dunger.Application.UseCases.Vehicles.Commands;
using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.Vehicles.CommandHandlers
{
    public class CreateVehiclesCommandHandler : ICommandHandler<CreateVehiclesCommand, Vehicle>
    {
        private readonly IAppDbContext _context;
        public CreateVehiclesCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<Vehicle> Handle(CreateVehiclesCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken);
            if (vehicle != null)
            {
                throw new Exception("Vehicle already exists");
            }

            vehicle = new Vehicle() { Name = request.Name };

            await _context.Vehicles.AddAsync(vehicle, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return vehicle;
        }
    }
}
