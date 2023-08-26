using Dunger.Application.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

namespace Dunger.Application.Services.TelegramBotKeyboards
{
    public class ReplyKeyboards
    {
        private readonly IAppDbContext _context;
        public ReplyKeyboards(IAppDbContext context)
        {
            _context = context;
        }
        public ReplyKeyboardMarkup BotOnReceivedStart()
        {
            ReplyKeyboardMarkup keyboardMarkup = new ReplyKeyboardMarkup(
                new[]
                {
                    new[] {new KeyboardButton("Menu"), new KeyboardButton("My Orders")},
                    new[] {new KeyboardButton("Ma'lumot"), new KeyboardButton("Fikr-Mulohozalar")},
                    new[] {new KeyboardButton("Aloqa"), new KeyboardButton("Sozlamalar")}
                });

            keyboardMarkup.ResizeKeyboard = true;

            return keyboardMarkup;
        }

        public ReplyKeyboardMarkup LanguageKeyboard()
        {
            List<KeyboardButton[]> buttonRows = new List<KeyboardButton[]>();
            var languages = _context.Languages.ToList();
            List<KeyboardButton> buttons = new List<KeyboardButton>();
            foreach (var language in languages)
            {
                if (buttons.Count == 2)
                {
                    buttonRows.Add(buttons.ToArray());
                    buttons = new List<KeyboardButton>();
                    continue;
                }

                buttons.Add(new KeyboardButton(language.Name.ToString()));
            }

            ReplyKeyboardMarkup keyboardMarkup = new ReplyKeyboardMarkup(buttons.ToArray());

            return keyboardMarkup;
        }
    }
}
