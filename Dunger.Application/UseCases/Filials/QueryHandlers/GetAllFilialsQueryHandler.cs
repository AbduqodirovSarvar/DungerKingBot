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
    public class GetAllFilialsQueryHandler : IQueryHandler<GetAllFilialsQuery, List<FilialViewModel>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public GetAllFilialsQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FilialViewModel>> Handle(GetAllFilialsQuery request, CancellationToken cancellationToken)
        {
            List<Filial>? filials = await _context.Filials.ToListAsync(cancellationToken);

            return _mapper.Map<List<FilialViewModel>>(filials);
        }
    }
}
