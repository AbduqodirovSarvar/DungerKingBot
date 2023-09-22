using AutoMapper;
using Dunger.Application.Abstractions;
using Dunger.Application.Models.ViewModels;
using Dunger.Application.UseCases.Menus.Commands;
using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.Menus.CommandHandlers
{
    public class UpdateMenuCommandHandler : ICommandHandler<UpdateMenuCommand, MenuViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public UpdateMenuCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;
        }
        public async Task<MenuViewModel> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            Menu? menu = await _context.Menus.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (menu == null)
            {
                throw new Exception("Menu not found");
            }

            menu.Price = request?.Price ?? menu.Price;
            menu.Description = request?.Description ?? menu.Description;
            menu.Name = request?.Name ?? menu.Name;
            if (request.)

            throw new NotImplementedException();
        }
    }
}
