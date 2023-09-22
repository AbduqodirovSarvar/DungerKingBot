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
    public class GetFeedbackQueryHandler : IQueryHandler<GetFeedbackQuery, FeedbackViewModel>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public GetFeedbackQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        async Task<FeedbackViewModel> IRequestHandler<GetFeedbackQuery, FeedbackViewModel>.Handle(GetFeedbackQuery request, CancellationToken cancellationToken)
        {
            Feedback? feedback = await _context.Feedbacks.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (feedback == null)
            {
                throw new Exception("Feedback not found");
            }

            return _mapper.Map<FeedbackViewModel>(feedback);
        }
    }
}
