using AutoMapper;
using Dunger.Application.Abstractions;
using Dunger.Application.Models.ViewModels;
using Dunger.Application.UseCases.Menus.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.Menus.QueryHandlers
{
    public class GetMenuQueryHandler : IQueryHandler<GetMenuQuery, MenuViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public GetMenuQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MenuViewModel> Handle(GetMenuQuery request, CancellationToken cancellationToken)
        {
            var menu = await _context.Menus.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (menu == null)
            {
                throw new Exception("Not Found Menu");
            }

            return _mapper.Map<MenuViewModel>(menu);
        }
    }
}
