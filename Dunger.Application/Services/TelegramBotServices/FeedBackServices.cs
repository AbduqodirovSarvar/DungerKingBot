﻿using Dunger.Application.Abstractions;
using Dunger.Application.Abstractions.TelegramBotAbstractions;
using Dunger.Application.Services.TelegramBotKeyboards;
using Dunger.Application.Services.TelegramBotMessages;
using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Dunger.Application.Services.TelegramBotServices
{
    public class FeedBackServices : IFeedBackServices
    {
        private readonly IAppDbContext _context;
        private readonly ISendMessageService _sendMsgService;
        private readonly Redis _redis;
        public FeedBackServices(IAppDbContext context, ISendMessageService sendMessageService, Redis redis)
        {
            _context = context;
            _sendMsgService = sendMessageService;
            _redis = redis;
        }

        public async Task CreateFeedBack(ITelegramBotClient botclient, Message message, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == message.Chat.Id, cancellationToken);
            if (user == null)
            {
                await botclient.SendTextMessageAsync(message.Chat.Id,
                    $"{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}\n{ReplyMessages.unAuthorized[0]}",
                    cancellationToken: cancellationToken);
                return;
            }

            Comment feedback = new()
            { 
                TelegramId = user.TelegramId,
                Message = message.Text ?? "Empty"
            };

            await _context.Comments.AddAsync(feedback, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await botclient.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: ReplyMessages.feedbackAnswer[user.LanguageId],
                    replyMarkup: ReplyKeyboards.MainPageKeyboards[user.LanguageId - 1],
                    cancellationToken: cancellationToken);

            await _sendMsgService.SendMessageToAdmins($"TelegramId: {feedback.TelegramId}\nFeedback: {feedback.Message}\n\n____{feedback.CreatedTime.ToString("dd-MM-yyyy HH:mm")}____");

            await _redis.DeleteState(feedback.TelegramId);

            return;
        }
    }
}