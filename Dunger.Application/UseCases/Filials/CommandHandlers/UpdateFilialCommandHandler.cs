using AutoMapper;
using Dunger.Application.Abstractions;
using Dunger.Application.Models.ViewModels;
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
    public class UpdateFilialCommandHandler : ICommandHandler<UpdateFilialCommand, FilialViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public UpdateFilialCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FilialViewModel> Handle(UpdateFilialCommand request, CancellationToken cancellationToken)
        {
            Filial? filial = await _context.Filials.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (filial == null)
            {
                throw new Exception("Filial not found");
            }

            filial.Name = request?.Name ?? filial.Name;
            filial.Description = request?.Description ?? filial.Description;
            filial.LocationUrl = request?.LocationUrl ?? filial.LocationUrl;
            filial.Address = request?.Address ?? filial.Address;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<FilialViewModel>(filial);
        }
    }
}
