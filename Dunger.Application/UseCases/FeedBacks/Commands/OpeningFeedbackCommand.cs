using Dunger.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dunger.Application.UseCases.FeedBacks.Commands
{
    public class OpeningFeedbackCommand : ICommand<bool>
    {
        public OpeningFeedbackCommand(int id) { FeedbackId = id; }
        [Required]
        public int FeedbackId { get; set; }
    }
}
