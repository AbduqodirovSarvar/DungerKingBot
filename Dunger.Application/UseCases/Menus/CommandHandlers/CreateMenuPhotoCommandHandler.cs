using AutoMapper;
using Dunger.Application.Abstractions;
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
    public class CreateMenuPhotoCommandHandler : ICommandHandler<CreateMenuPhotoCommand, MenuPhoto>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public CreateMenuPhotoCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<MenuPhoto> Handle(CreateMenuPhotoCommand request, CancellationToken cancellationToken)
        {
            var menu = await _context.Menus.FirstOrDefaultAsync(x => x.Id == request.MenuId, cancellationToken);
            if (menu == null)
            {
                throw new Exception("Not found menu");
            }
            var photo = await _context.MenuPhotos.FirstOrDefaultAsync(x => x.MenuId == menu.Id, cancellationToken);
            if (photo == null)
            {
                photo = _mapper.Map<MenuPhoto>(request);
                await _context.MenuPhotos.AddAsync(photo, cancellationToken);
            }

            photo.Title = request.Title;
            photo.PhotoPath = request.PhotoPath;

            await _context.SaveChangesAsync(cancellationToken);

            return photo;
        }
    }
}
