using Telegram.Bot.Types.ReplyMarkups;

namespace Dunger.Application.Services.TelegramServices.TelegramBotKeyboards
{
    public class InlineKeyboards
    {
        private static readonly string[] next = new[] { "Keyingis", "Next", "Следующий" };
        private static readonly string[] previous = new[] { "Avvalgisi", "Previous", "Предыдущий" };
        private static readonly string[] back = new[] { "Orqaga", "Back", "Назад" };
        public static InlineKeyboardMarkup[] OrderGetButtons
        {
            get
            {
                return new[]{
                    MakingInlineKeyboard(new List<string> { next[0], previous[0]}),
                    MakingInlineKeyboard(new List<string> { next[1], previous[1]}),
                    MakingInlineKeyboard(new List<string> { next[2], previous[2]})
                };
            }
        }

        public static InlineKeyboardMarkup[] BackButtons { get => backButtons; set => backButtons = value; }

        private static InlineKeyboardMarkup[] backButtons = new[]
        {
            MakingInlineKeyboard(null, back[0], "back", 1),
            MakingInlineKeyboard(null, back[1], "back", 2),
            MakingInlineKeyboard(null, back[2], "back", 2)
        };
        public static InlineKeyboardMarkup MakingInlineKeyboard(List<string>? names = null, string? thename = null, string? thecallbackdata = null, int? rows = 3)
        {
            List<InlineKeyboardButton[]> buttonRows = new();
            List<InlineKeyboardButton> buttons = new();
            if (names != null)
            {
                foreach (var name in names)
                {
                    if (buttons.Count == rows)
                    {
                        buttonRows.Add(buttons.ToArray());
                        buttons.Clear();
                    }

                    buttons.Add(InlineKeyboardButton.WithCallbackData(name.ToString(), name.ToLower()));
                }

                if (buttons.Count > 0)
                {
                    buttonRows.Add(buttons.ToArray());
                }

                return new InlineKeyboardMarkup(buttonRows.ToArray());
            }
            else if (thename != null)
            {
                return new InlineKeyboardMarkup(new[] { InlineKeyboardButton.WithCallbackData(thename, thecallbackdata ?? thename) });
            }

            return new InlineKeyboardMarkup(Array.Empty<InlineKeyboardButton[]>());
        }
    }
}
