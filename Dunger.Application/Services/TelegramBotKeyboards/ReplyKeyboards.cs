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
        public static ReplyKeyboardMarkup BotOnReceivedStart
        {
            get
            {
                ReplyKeyboardMarkup keyboardMarkup = new(
                    new[]
                    {
                    new[] {new KeyboardButton("Menu"), new KeyboardButton("My Orders")},
                    new[] {new KeyboardButton("Ma'lumot"), new KeyboardButton("Fikr-Mulohozalar")},
                    new[] {new KeyboardButton("Aloqa"), new KeyboardButton("Sozlamalar")}
                    })
                {
                    ResizeKeyboard = true
                };

                return keyboardMarkup;
            }
        }

        public ReplyKeyboardMarkup LanguageKeyboard
        {
            get
            {
                /*List<KeyboardButton[]> buttonRows = new();
                var languages = _context.Languages.ToList();
                List<KeyboardButton> buttons = new();
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

                ReplyKeyboardMarkup keyboardMarkup = new(buttons.ToArray())
                {
                    ResizeKeyboard = true
                };

                return keyboardMarkup;*/

                List<KeyboardButton[]> buttonRows = new List<KeyboardButton[]>();
                var languages = _context.Languages.ToList();
                List<KeyboardButton> buttons = new List<KeyboardButton>();
                foreach (var language in languages)
                {
                    if (buttons.Count == 2)
                    {
                        buttonRows.Add(buttons.ToArray());
                        buttons.Clear();
                    }

                    buttons.Add(new KeyboardButton(language.Name.ToString()));
                }

                if (buttons.Count > 0)
                {
                    buttonRows.Add(buttons.ToArray());
                }

                ReplyKeyboardMarkup keyboardMarkup = new ReplyKeyboardMarkup(buttonRows.ToArray())
                {
                    ResizeKeyboard = true
                };

                return keyboardMarkup;
            }
        }
    }
}
