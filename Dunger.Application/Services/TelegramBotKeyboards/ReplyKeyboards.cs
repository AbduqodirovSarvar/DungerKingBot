using Dunger.Application.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Dunger.Application.Services.TelegramBotKeyboards
{
    public class ReplyKeyboards
    {
        private static readonly string[] buttonUz = new[] { "Menyu", "Buyurtmalarim", "Biz haqimizda", "Fikr bildirish", "Aloqa", "Sozlamalar" };
        private static readonly string[] buttonEn = new[] { "Menu", "My Orders", "About Us", "Feedback", "Contact", "Settings" };
        private static readonly string[] buttonRu = new[] { "Меню", "Мои заказы", "О нас", "Обратная связь", "Контакт", "Настройки" };
        private static readonly string[] aboutUz = new[] { "Filiallar", "Qadriyatlarimiz", "Vakansiyalar", "Orqaga" };
        private static readonly string[] aboutEn = new[] { "Filials", "Our principles", "Vacancies", "Back" };
        private static readonly string[] aboutRu = new[] { "Ветви", "Наши ценности", "Вакансии", "Назад" };
        public static readonly string[] back = new[] { "", "Orqaga", "Back", "Назад" };
        public static readonly ReplyKeyboardMarkup[] MainPageKeyboards = new[] { MakingKeyboard(buttonUz.ToList()), MakingKeyboard(buttonEn.ToList()), MakingKeyboard(buttonRu.ToList()) };
        public static readonly ReplyKeyboardMarkup[] AboutPageKeyboards = new[] { MakingKeyboard(aboutUz.ToList()), MakingKeyboard(aboutEn.ToList()), MakingKeyboard(aboutRu.ToList()) };
        public static readonly ReplyKeyboardMarkup[] BackKeyboards = new[] { MakingKeyboard(null, back[0]), MakingKeyboard(null, back[1]), MakingKeyboard(null, back[2]) };

        //private static WebAppInfo MenuPageWebApp = new(){ Url = "https://lms.tuit.uz/auth/login" };
        //private static readonly WebAppInfo SettingsPageWebApp = new() { Url = "https://lms.tuit.uz/auth/login" };

        public static ReplyKeyboardMarkup MakingKeyboard(List<string>? names = null, string? thename = null, int? rows = 2)
        {
            List<KeyboardButton[]> buttonRows = new();
            List<KeyboardButton> buttons = new();
            if (names != null)
            {
                foreach (var name in names)
                {
                    if(name == "Menyu"|| name == "Menu" || name == "Меню")
                    {
                        buttons.Add( new KeyboardButton(name.ToString()) { WebApp = new WebAppInfo() { Url = "https://lms.tuit.uz/auth/login" } });

                        if (buttons.Count == rows)
                        {
                            buttonRows.Add(buttons.ToArray());
                            buttons.Clear();
                        }
                        continue;
                    }

                    if (buttons.Count == rows)
                    {
                        buttonRows.Add(buttons.ToArray());
                        buttons.Clear();
                    }

                    buttons.Add(new KeyboardButton(name.ToString()));
                }

                if (buttons.Count > 0)
                {
                    buttonRows.Add(buttons.ToArray());
                }

                return new ReplyKeyboardMarkup(buttonRows.ToArray()) {ResizeKeyboard = true };
            }
            else if (thename != null)
            {
                return new ReplyKeyboardMarkup(new[] { new KeyboardButton(thename.ToString()) }) { ResizeKeyboard = true };
            }

            return new ReplyKeyboardMarkup(Array.Empty<KeyboardButton[]>());
        }


        private static KeyboardButton MakingReplyButtonForWebApp(string name, WebAppInfo webapp)
        {
            KeyboardButton replyKeyboard = new(name.ToString())
            {
                WebApp = webapp,
            };

            return replyKeyboard;
        }
    }
}
