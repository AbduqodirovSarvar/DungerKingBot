using Dunger.Application.Abstractions;
using Dunger.Application.UseCases.Menus.Commands;
using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dunger.Application.UseCases.Menus.CommandHandlers
{
    public class DeleteMenuCommandHandler : ICommandHandler<DeleteMenuCommand, bool>
    {
        private readonly IAppDbContext _context;
        public DeleteMenuCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            Menu? menu = await _context.Menus.FirstOrDefaultAsync(x => x.Id == request.MenuId, cancellationToken);
            if (menu == null)
            {
                return true;
            }

            _context.Menus.Remove(menu);
            return (await _context.SaveChangesAsync(cancellationToken)) > 0;
        }
    }
}
