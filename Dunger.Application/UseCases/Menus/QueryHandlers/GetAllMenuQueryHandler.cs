using AutoMapper;
using Dunger.Application.Abstractions;
using Dunger.Application.Models.ViewModels;
using Dunger.Application.UseCases.Menus.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.Menus.QueryHandlers
{
    public class GetAllMenuQueryHandler : IQueryHandler<GetAllMenuQuery, List<MenuViewModel>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public GetAllMenuQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MenuViewModel>> Handle(GetAllMenuQuery request, CancellationToken cancellationToken)
        {
            var menus = await _context.Menus.ToListAsync(cancellationToken);

            return _mapper.Map<List<MenuViewModel>>(menus);
        }
    }
}
