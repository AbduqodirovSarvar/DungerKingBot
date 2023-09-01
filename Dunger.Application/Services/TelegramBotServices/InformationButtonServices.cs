using Dunger.Application.Abstractions;
using Dunger.Application.Abstractions.TelegramBotAbstractions;
using Dunger.Application.Services.TelegramBotKeyboards;
using Dunger.Application.Services.TelegramBotMessages;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Dunger.Application.Services.TelegramBotServices
{
    public class InformationButtonServices : IInformationButtonServices
    {
        private readonly ITelegramBotClient _client;
        private readonly IAppDbContext _context;
        public InformationButtonServices(ITelegramBotClient client, IAppDbContext context)
        {
            _client = client;
            _context = context;
        }

        public async Task CatchMessageFromAbout(Message message, int langauageId, CancellationToken cancellationToken = default)
        {
            var result = message.Text switch
            {
                "Filiallar" or "Filials" or "Ветви" => AboutFilials(message, langauageId, cancellationToken),
                "Our principles" or "Qadriyatlarimiz" or "Наши ценности" => AboutPrinciples(message, langauageId, cancellationToken),
                "Vacancies" or "Вакансии" or "Vakansiyalar" => AboutVacancies(message, langauageId, cancellationToken),
                "Orqaga" or "Back" or "Назад" => BackButton(message, langauageId, cancellationToken),
                _ => BackButton(message, langauageId, cancellationToken)
            };

            await result;

            return;
        }

        private async Task BackButton(Message message, int langauageId, CancellationToken cancellationToken = default)
        {
            await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                text: ReplyMessages.chooseCommand[langauageId],
                replyMarkup: ReplyKeyboards.MainPageKeyboards[langauageId - 1],
                cancellationToken: cancellationToken);

            await Redis.DeleteState(message.Chat.Id);

            return;
        }

        private async Task AboutFilials(Message message, int langauageId, CancellationToken cancellationToken = default)
        {
            List<string> filials = await _context.Filials.Select(x => x.Name).ToListAsync(cancellationToken);

            await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                text: ReplyMessages.chooseFilial[langauageId],
                replyMarkup: InlineKeyboards.MakingInlineKeyboard(filials),
                cancellationToken: cancellationToken);

            await Redis.SetUserState(message.Chat.Id, "about");

            return;
        }

        private async Task AboutPrinciples(Message message, int langauageId, CancellationToken cancellationToken = default)
        {
            await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                text: "Bizning qadriyatlarimiz!",
                replyMarkup: ReplyKeyboards.AboutPageKeyboards[langauageId - 1],
                cancellationToken: cancellationToken);

            await Redis.SetUserState(message.Chat.Id, "about");

            return;
        }

        public async Task AboutTheFilial(CallbackQuery callbackQuery, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == callbackQuery.From.Id, cancellationToken);
            if (user == null)
            {
                return;
            }
            var filial = await _context.Filials.FirstOrDefaultAsync(x => x.Name.ToLower() == callbackQuery.Data!.ToLower(), cancellationToken);

            if (filial == null)
            {
                await Redis.SetUserState(callbackQuery.From.Id, "about");
                return;
            }

            await _client.SendTextMessageAsync(chatId: callbackQuery.From.Id,
                text: ReplyMessages.MakingAboutFilialText(filial, user.LanguageId),
                cancellationToken: cancellationToken);

            await Redis.SetUserState(callbackQuery.From.Id, "about");
            return;
        }

        private async Task AboutVacancies(Message message, int langauageId, CancellationToken cancellationToken = default)
        {
            await _client.SendTextMessageAsync(chatId: message.Chat.Id,
                text: "Hozircha vakansiyalarimiz yo'q",
                replyMarkup: ReplyKeyboards.AboutPageKeyboards[langauageId - 1],
                cancellationToken: cancellationToken);

            await Redis.SetUserState(message.Chat.Id, "about");

            return;
        }
    }
}
