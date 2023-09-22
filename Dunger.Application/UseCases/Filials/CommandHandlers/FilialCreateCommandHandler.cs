using AutoMapper;
using Dunger.Application.Abstractions;
using Dunger.Application.Models.ViewModels;
using Dunger.Application.UseCases.Filials.Commands;
using Dunger.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.Filials.CommandHandlers
{
    public class FilialCreateCommandHandler : ICommandHandler<FilialCreateCommand, FilialViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public FilialCreateCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        async Task<FilialViewModel> IRequestHandler<FilialCreateCommand, FilialViewModel>.Handle(FilialCreateCommand request, CancellationToken cancellationToken)
        {
            var filial = await _context.Filials.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
            if (filial != null)
            {
                throw new Exception("Already exists");
            }

            filial = _mapper.Map<Filial>(request);

            await _context.Filials.AddAsync(filial, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<FilialViewModel>(filial);
        }
    }
}
