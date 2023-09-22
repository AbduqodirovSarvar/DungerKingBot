using Dunger.Application.Abstractions;
using Dunger.Application.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.FeedBacks.Queries
{
    public class GetAllFeedBackQuery : IQuery<List<FeedbackViewModel>>
    {
        public GetAllFeedBackQuery() { }
        public bool? IsOpened { get; set; } = null;
    }
}
