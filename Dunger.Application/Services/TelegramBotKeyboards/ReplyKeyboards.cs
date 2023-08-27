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

        private static readonly string[] buttonUz = new[] { "Menyu", "Buyurtmalarim", "Biz haqimizda", "Fikr bildirish", "Aloqa", "Sozlamalar" };
        private static readonly string[] buttonEn = new[] { "Menu", "My Orders", "About Us", "Feedback", "Contact", "Settings" };
        private static readonly string[] buttonRu = new[] { "Меню", "Мои заказы", "О нас", "Обратная связь", "Контакт", "Настройки" };
        private static readonly string[] aboutUz = new[] { "Filallar", "Qadriyatlarimiz", "Vakansiyalar", "Orqaga" };
        private static readonly string[] aboutEn = new[] { "Filials", "Our principles", "Vacancies", "Back" };
        private static readonly string[] aboutRu = new[] { "Ветви", "Наши ценности", "Вакансии", "Назад" };
        public static readonly ReplyKeyboardMarkup[] MainPageKeyboards = new[] { MainPageKeyboardsUz, MainPageKeyboardsEn, MainPageKeyboardsRu };
        public static readonly ReplyKeyboardMarkup[] AboutPageKeyboards = new[] {AboutUz, AboutEn, AboutRu };

        private static ReplyKeyboardMarkup MainPageKeyboardsUz
        {
            get
            {
                ReplyKeyboardMarkup keyboardMarkup = new(
                    new[]
                    {
                    new[] {new KeyboardButton(buttonUz[0]), new KeyboardButton(buttonUz[1])},
                    new[] {new KeyboardButton(buttonUz[2]), new KeyboardButton(buttonUz[3])},
                    new[] {new KeyboardButton(buttonUz[4]), new KeyboardButton(buttonUz[5])}
                    })
                {
                    ResizeKeyboard = true
                };

                return keyboardMarkup;
            }
        }
        private static ReplyKeyboardMarkup MainPageKeyboardsEn
        {
            get
            {
                ReplyKeyboardMarkup keyboardMarkup = new(
                    new[]
                    {
                    new[] {new KeyboardButton(buttonEn[0]), new KeyboardButton(buttonEn[1])},
                    new[] {new KeyboardButton(buttonEn[2]), new KeyboardButton(buttonEn[3])},
                    new[] {new KeyboardButton(buttonEn[4]), new KeyboardButton(buttonEn[5])}
                    })
                {
                    ResizeKeyboard = true
                };

                return keyboardMarkup;
            }
        }
        private static ReplyKeyboardMarkup MainPageKeyboardsRu
        {
            get
            {
                ReplyKeyboardMarkup keyboardMarkup = new(
                    new[]
                    {
                    new[] {new KeyboardButton(buttonRu[0]), new KeyboardButton(buttonRu[1])},
                    new[] {new KeyboardButton(buttonRu[2]), new KeyboardButton(buttonRu[3])},
                    new[] {new KeyboardButton(buttonRu[4]), new KeyboardButton(buttonRu[5])}
                    })
                {
                    ResizeKeyboard = true
                };

                return keyboardMarkup;
            }
        }



        private static ReplyKeyboardMarkup AboutUz
        {
            get
            {
                ReplyKeyboardMarkup keyboardMarkup = new(
                    new[]
                    {
                    new[] {new KeyboardButton(aboutUz[0]), new KeyboardButton(aboutUz[1])},
                    new[] {new KeyboardButton(aboutUz[2])},
                    new[] {new KeyboardButton(aboutUz[3])}
                    })
                {
                    ResizeKeyboard = true
                };

                return keyboardMarkup;
            }
        }
        private static ReplyKeyboardMarkup AboutEn
        {
            get
            {
                ReplyKeyboardMarkup keyboardMarkup = new(
                    new[]
                    {
                    new[] {new KeyboardButton(aboutEn[0]), new KeyboardButton(aboutEn[1])},
                    new[] {new KeyboardButton(aboutEn[2])},
                    new[] {new KeyboardButton(aboutEn[3])}
                    })
                {
                    ResizeKeyboard = true
                };

                return keyboardMarkup;
            }
        }
        private static ReplyKeyboardMarkup AboutRu
        {
            get
            {
                ReplyKeyboardMarkup keyboardMarkup = new(
                    new[]
                    {
                    new[] {new KeyboardButton(aboutRu[0]), new KeyboardButton(aboutRu[1])},
                    new[] {new KeyboardButton(aboutRu[2])},
                    new[] {new KeyboardButton(aboutRu[3])}
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
                List<KeyboardButton[]> buttonRows = new();
                var languages = _context.Languages.ToList();
                List<KeyboardButton> buttons = new();
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

                ReplyKeyboardMarkup keyboardMarkup = new (buttonRows.ToArray())
                {
                    ResizeKeyboard = true
                };

                return keyboardMarkup;
            }
        }
    }
}
