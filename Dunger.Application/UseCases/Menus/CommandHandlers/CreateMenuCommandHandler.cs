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
    public class CreateMenuCommandHandler : ICommandHandler<CreateMenuCommand, MenuViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public CreateMenuCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<MenuViewModel> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            Menu? menu = await _context.Menus.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
            if (menu != null)
            {
                throw new Exception();
            }

            menu = _mapper.Map<Menu>(request);

            await _context.Menus.AddAsync(menu, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            MenuPhoto menuPhoto = new MenuPhoto();
            menuPhoto.PhotoPath = request.PhotoPath!;
            menuPhoto.Title = request.Title!;

            menuPhoto.MenuId = (await _context.Menus.FirstOrDefaultAsync(x => x.Name == menu.Name, cancellationToken))?.Id ?? 1;

            await _context.MenuPhotos.AddAsync(menuPhoto, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<MenuViewModel>(menu);
        }
    }
}
