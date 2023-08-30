﻿using Dunger.Application.Abstractions;
using Dunger.Application.Abstractions.TelegramBotAbstractions;
using Dunger.Application.Services.TelegramBotKeyboards;
using Dunger.Application.Services.TelegramBotMessages;
using Dunger.Application.Services.TelegramBotStates;
using Dunger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Dunger.Application.Services.TelegramBotServices
{
    public class RegisterService : IRegisterService
    {
        private readonly Redis _redis;
        private readonly IAppDbContext _context;
        private readonly ITelegramBotClient _client;
        private readonly ILogger<RegisterService> _logger;
        public RegisterService(Redis redis, IAppDbContext context, ITelegramBotClient client, ILogger<RegisterService> logger)
        {
            _redis = redis;
            _context = context;
            _client = client;
            _logger = logger;
        }
        public static Domain.Entities.User? UserObject { get; set; }
        public async Task CatchMessageFromRegister(Message message, CancellationToken cancellationToken = default)
        {
            List<string> languages = await _context.Languages.Select(x => x.Name).ToListAsync(cancellationToken);
            var state = await _redis.GetUserState(message.Chat.Id);
            await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: $"Tilni tanglang!\nChoose language!\nВыберите язык!",
                    replyMarkup: ReplyKeyboards.MakingKeyboard(languages),
                    cancellationToken: cancellationToken);

            await _redis.SetUserState(message.Chat.Id, State.states[0]);// "language" state

            UserObject = new Domain.Entities.User
            {
                UserName = message.Chat.Username,
                TelegramId = message.Chat.Id
            };

            return;
        }

        public async Task SendContact(Domain.Entities.User user, Message message, CancellationToken cancellationToken = default)
        {
            if (user == null || message.Contact == null || message.Contact.PhoneNumber == null)
            {
                return;
            }

            user.Phone = message.Contact.PhoneNumber;

            await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: ReplyMessages.afterRegistered[user.LanguageId],
                    replyMarkup: ReplyKeyboards.MainPageKeyboards[user.LanguageId - 1],
                    cancellationToken: cancellationToken);

            await _redis.DeleteState(message.Chat.Id);

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            UserObject = null;

            return;
        }

        public async Task SendFirstName(Domain.Entities.User user, Message message, CancellationToken cancellationToken = default)
        {
            user.FirstName = message.Text ?? $"{message.Chat.FirstName}";

            await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: ReplyMessages.askLastName[user.LanguageId],
                    cancellationToken: cancellationToken);

            await _redis.Next(message.Chat.Id);

            return;
        }

        public async Task SendLastName(Domain.Entities.User user, Message message, CancellationToken cancellationToken)
        {
            user.LastName = message.Text ?? $"{message.Chat.LastName}";

            ReplyKeyboardMarkup replyKeyboard = new(
                new[]
                {
                    new KeyboardButton[] { new KeyboardButton(ReplyMessages.shareContact[user.LanguageId])
                    { RequestContact = true } }
                })
                {
                    ResizeKeyboard = true
                };

            await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: ReplyMessages.askContact[user.LanguageId],
                    replyMarkup: replyKeyboard,
                    cancellationToken: cancellationToken);

            await _redis.Next(message.Chat.Id);

            return;
        }

        public async Task SendLanguage(Domain.Entities.User user, Message message, CancellationToken cancellationToken)
        {
            try
            {
                if (user == null)
                {
                    return;
                }

                Console.WriteLine($"CT: {cancellationToken}");
                Language? language;
                try
                {
                    language = await _context.Languages.FirstOrDefaultAsync(x => x.Name.ToLower() == message.Text!.ToLower(), cancellationToken);
                }
                catch (Exception)
                {
                    language = _context.Languages.FirstOrDefault(x => x.Name.ToLower() == message.Text!.ToLower());
                }
                

                user.LanguageId = language?.Id ?? 1;

                await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: ReplyMessages.askFirstName[user.LanguageId],
                    replyMarkup: new ReplyKeyboardRemove(),
                    cancellationToken: cancellationToken);

                await _redis.Next(message.Chat.Id);

                return;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ERROR:  {ex}", ex.Message);
                return;
            }
        }
    }
}