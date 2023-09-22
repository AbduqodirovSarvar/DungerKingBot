using Dunger.Application.Abstractions;
using Dunger.Application.UseCases.FeedBacks.Commands;
using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.UseCases.FeedBacks.CommandHandlers
{
    public class OpeningFeedbackCommandHandler : ICommandHandler<OpeningFeedbackCommand, bool>
    {
        private readonly IAppDbContext _context;
        public OpeningFeedbackCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(OpeningFeedbackCommand request, CancellationToken cancellationToken)
        {
            Feedback? feedback = await _context.Feedbacks.FirstOrDefaultAsync(x => x.Id == request.FeedbackId, cancellationToken);
            if (feedback == null)
            {
                throw new Exception("Feedback not found");
            }

            feedback.IsOpened = true;
            return (await _context.SaveChangesAsync(cancellationToken)) > 0;
        }
    }
}
