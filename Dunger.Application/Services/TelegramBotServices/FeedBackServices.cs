using Dunger.Application.Abstractions;
using Dunger.Application.Abstractions.TelegramBotAbstractions;
using Dunger.Application.Services.TelegramBotKeyboards;
using Dunger.Application.Services.TelegramBotMessages;
using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Dunger.Application.Services.TelegramBotServices
{
    public class FeedBackServices : IFeedBackServices
    {
        private readonly IAppDbContext _context;
        private readonly ISendMessageService _sendMsgService;
        private readonly ITelegramBotClient _client;
        public FeedBackServices(IAppDbContext context, ISendMessageService sendMessageService, ITelegramBotClient client)
        {
            _context = context;
            _sendMsgService = sendMessageService;
            _client = client;
        }

        public async Task CreateFeedBack(Message message, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == message.Chat.Id, cancellationToken);
            if (user == null)
            {
                await _client.SendTextMessageAsync(message.Chat.Id,
                    $"{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}",
                    cancellationToken: cancellationToken);
                return;
            }

            Feedback feedback = new()
            {
                TelegramId = user.TelegramId,
                Message = message.Text ?? "Empty"
            };

            await _context.Feedbacks.AddAsync(feedback, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: ReplyMessages.feedbackAnswer[user.LanguageId],
                    replyMarkup: ReplyKeyboards.MainPageKeyboards[user.LanguageId - 1],
                    cancellationToken: cancellationToken);

            await _sendMsgService.SendMessageToAdmins($"TelegramId: {feedback.TelegramId}\nFeedback: {feedback.Message}\n\n____{feedback.CreatedTime.ToString("dd-MM-yyyy HH:mm")}____");

            await Redis.DeleteState(feedback.TelegramId);

            return;
        }
    }
}
