using AutoMapper;
using Dunger.Application.Abstractions;
using Dunger.Application.Models.ViewModels;
using Dunger.Application.UseCases.FeedBacks.Queries;
using Dunger.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.FeedBacks.QueryHandlers
{
    public class GetAllFeedbackQueryHandler : IQueryHandler<GetAllFeedBackQuery, List<FeedbackViewModel>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public GetAllFeedbackQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        async Task<List<FeedbackViewModel>> IRequestHandler<GetAllFeedBackQuery, List<FeedbackViewModel>>.Handle(GetAllFeedBackQuery request, CancellationToken cancellationToken)
        {
            List<Feedback> feedbacks = await _context.Feedbacks.ToListAsync(cancellationToken);

            if(request.IsOpened != null)
            {
                feedbacks = feedbacks.Where(x => x.IsOpened == request.IsOpened).ToList();
            }

            return _mapper.Map<List<FeedbackViewModel>>(feedbacks);
        }
    }
}
