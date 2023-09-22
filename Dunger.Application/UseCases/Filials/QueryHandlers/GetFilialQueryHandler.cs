using AutoMapper;
using Dunger.Application.Abstractions;
using Dunger.Application.Models.ViewModels;
using Dunger.Application.UseCases.Filials.Queries;
using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.Filials.QueryHandlers
{
    public class GetFilialQueryHandler : IQueryHandler<GetFilialQuery, FilialViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public GetFilialQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FilialViewModel> Handle(GetFilialQuery request, CancellationToken cancellationToken)
        {
            Filial? filial = await _context.Filials.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (filial == null)
            {
                throw new Exception("Filial not found");
            }

            return _mapper.Map<FilialViewModel>(filial);
        }
    }
}
