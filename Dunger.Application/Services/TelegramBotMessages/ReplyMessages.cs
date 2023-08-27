using Dunger.Application.Services.TelegramBotKeyboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunger.Application.Services.TelegramBotMessages
{
    public class ReplyMessages
    {
        public static readonly string[] unAuthorized = new[] {"", "Bu buyruqdan foydalanish uchun ro'yhatdan o'ting!", "Register to use this command!", "Зарегистрируйтесь, чтобы использовать эту команду!" };
        public static readonly string[] afterRegistered = new[] {"", "Tabriklaymiz!\nSiz ro'yhatdan muvaffaqiyatli o'tdingiz!\nKerakli bo'limni tanlang!", "Congratulations!\nYou have successfully registered!\nChoose the required section!", "Поздравляем!\nВы успешно зарегистрировались!\nВыберите нужный раздел!" };
        public static readonly string[] askFirstName = new[] {"", "Assalomu aleykum\nBotimizga hush kelibsiz!\n\nBotdan foydalanish uchun ro'yxatdan o'ting!\n\nIsmingizni kiriting:", "Hello\nWelcome to our bot!\n\nRegister to use the bot!\n\nEnter your first name:", "Привет\nДобро пожаловать к нашему боту!\n\nЗарегистрируйтесь, чтобы использовать бот!\n\nВведите свое имя:" };
        public static readonly string[] askLastName = new[] {"", "Familiyangizni kiriting:", "Enter your last name:", "Введите свою фамилию:" };
        public static readonly string[] askContact = new[] {"", "Kantaktingizni yuboring:", "Send your contact:", "Отправьте свой контакт:" };
        public static readonly string[] shareContact = new[] {"","Kontaktni ulashish", "Share Contact", "Поделиться контактом"};
        public static readonly string[] chooseCommand = new[] { "", "Kerakli bo'limni tanlang:", "Select the desired section:", "Выберите нужный раздел:" };
        public static readonly string[] askFeedback = new[] { "", "Taklif va shikoyatlaringizni yozing.\nBiz ularni albatta ko'rib chiqamiz!", "Write your suggestions and complaints.\nWe will definitely consider them!", "Пишите свои предложения и жалобы.\nМы обязательно их рассмотрим!" };
        public static readonly string[] unKnownCommand = new[] { "", "Siz belgilanmagan buyruqni yubordingiz!", "You sent an unsigned command!", "Вы отправили неподписанную команду!" };
        public static readonly string[] feedbackAnswer = new[] { "", "Befarq bo'lmaganingiz uchun raxmat!", "Thank you for not being indifferent!", "Спасибо, что не остались равнодушными!" };

    }
}
