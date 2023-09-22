using AutoMapper;
using Dunger.Application.Abstractions;
using Dunger.Application.UseCases.Filials.Commands;
using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.Filials.CommandHandlers
{
    public class DeleteFilialCommandHandler : ICommandHandler<DeleteFilialCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public DeleteFilialCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Handle(DeleteFilialCommand request, CancellationToken cancellationToken)
        {
            Filial? filial = await _context.Filials.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (filial == null)
            {
                throw new Exception("Filial not found");
            }

            _context.Filials.Remove(filial);
            return (await _context.SaveChangesAsync(cancellationToken)) > 0;
        }
    }
}
