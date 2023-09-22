using Dunger.Application.Abstractions;
using Dunger.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.FeedBacks.Queries
{
    public class GetFeedbackQuery : IQuery<FeedbackViewModel>
    {
        public GetFeedbackQuery(int id) { Id = id; }
        [Required]
        public int Id { get; set; }
    }
}
