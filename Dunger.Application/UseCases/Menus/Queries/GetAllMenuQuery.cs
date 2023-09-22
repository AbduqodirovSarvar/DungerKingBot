using Dunger.Application.Abstractions;
using Dunger.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.Menus.Queries
{
    public class GetAllMenuQuery : IQuery<List<MenuViewModel>>
    {
        public GetAllMenuQuery() { }
    }
}
