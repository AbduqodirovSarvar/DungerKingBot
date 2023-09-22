using Dunger.Application.Abstractions;
using Dunger.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.Filials.Queries
{
    public class GetFilialQuery : IQuery<FilialViewModel>
    {
        public GetFilialQuery(int id) { Id = id; }
        [Required]
        public int Id { get; set; }
    }
}
