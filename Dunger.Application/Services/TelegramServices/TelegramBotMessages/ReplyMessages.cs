using Dunger.Domain.Entities;
using System.Text;

namespace Dunger.Application.Services.TelegramServices.TelegramBotMessages
{
    public class ReplyMessages
    {

        public static readonly string[] unAuthorized = new[] { "", "Bu buyruqdan foydalanish uchun ro'yhatdan o'ting!", "Register to use this command!", "Зарегистрируйтесь, чтобы использовать эту команду!" };
        public static readonly string[] afterRegistered = new[] { "", "Tabriklaymiz!\nSiz ro'yhatdan muvaffaqiyatli o'tdingiz!\nKerakli bo'limni tanlang!", "Congratulations!\nYou have successfully registered!\nChoose the required section!", "Поздравляем!\nВы успешно зарегистрировались!\nВыберите нужный раздел!" };
        public static readonly string[] askFirstName = new[] { "", "Assalomu aleykum\nBotimizga hush kelibsiz!\n\nBotdan foydalanish uchun ro'yxatdan o'ting!\n\nIsmingizni kiriting:", "Hello\nWelcome to our bot!\n\nRegister to use the bot!\n\nEnter your first name:", "Привет\nДобро пожаловать к нашему боту!\n\nЗарегистрируйтесь, чтобы использовать бот!\n\nВведите свое имя:" };
        public static readonly string[] askLastName = new[] { "", "Familiyangizni kiriting:", "Enter your last name:", "Введите свою фамилию:" };
        public static readonly string[] askContact = new[] { "", "Kantaktingizni yuboring:", "Send your contact:", "Отправьте свой контакт:" };
        public static readonly string[] shareContact = new[] { "", "Kontaktni ulashish", "Share Contact", "Поделиться контактом" };
        public static readonly string[] chooseCommand = new[] { "", "Kerakli bo'limni tanlang:", "Select the desired section:", "Выберите нужный раздел:" };
        public static readonly string[] askFeedback = new[] { "", "Taklif va shikoyatlaringizni yozing.\nBiz ularni albatta ko'rib chiqamiz!", "Write your suggestions and complaints.\nWe will definitely consider them!", "Пишите свои предложения и жалобы.\nМы обязательно их рассмотрим!" };
        public static readonly string[] unKnownCommand = new[] { "", "Siz belgilanmagan buyruqni yubordingiz!", "You sent an unsigned command!", "Вы отправили неподписанную команду!" };
        public static readonly string[] feedbackAnswer = new[] { "", "Befarq bo'lmaganingiz uchun raxmat!", "Thank you for not being indifferent!", "Спасибо, что не остались равнодушными!" };
        public static readonly string[] chooseFilial = new[] { "", "Filialni tanglang:", "Choose Filial:", "Выберите филиал:" };
        public static readonly string[] emptyOrders = new[] { "", "Sizning buyurtmalaringiz yo'q!", "You have no orders!", "У вас нет заказов!" };
        public static readonly string[] helpMessages = new[] { "", "Assalomu aleykum\nBotimizdan foydalanish uchun /start buyrug'ini bosing!", "Hello\nClick /start to use our bot!", "Здравствуйте!\nНажмите /начать, чтобы использовать нашего бота!" };

        public static List<string> OrderMessages(List<Order> orders, int? LanguageId = 1, CancellationToken cancellationToken = default)
        {
            List<string> messages = LanguageId switch
            {
                1 => OrdersMessageUz(orders, cancellationToken),
                2 => OrdersMessageEn(orders, cancellationToken),
                3 => OrdersMessageRu(orders, cancellationToken),
                _ => OrdersMessageUz(orders, cancellationToken)
            };

            return messages;
        }
        public static string MakingAboutFilialText(Filial filial, int LanguageId)
        {
            return LanguageId switch
            {
                1 => aboutFilialUz(filial),
                2 => aboutFilialEn(filial),
                3 => aboutFilialRu(filial),
                _ => aboutFilialUz(filial)
            };
        }

        private static string aboutFilialUz(Filial filial)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("Filial nomi: " + filial.Name);
            result.AppendLine("Filial manzili: " + filial.Address);
            result.AppendLine("Filial manzili xaritada: " + filial.LocationUrl);
            result.AppendLine("Filial bilan bog'lanish uchun: ...");

            return result.ToString();
        }
        private static string aboutFilialEn(Filial filial)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("Branch Name: " + filial.Name);
            result.AppendLine("Branch address: " + filial.Address);
            result.AppendLine("Branch address on the map: " + filial.LocationUrl);
            result.AppendLine("To contact the branch: ...");

            return result.ToString();
        }
        private static string aboutFilialRu(Filial filial)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("Название филиала: " + filial.Name);
            result.AppendLine("Адрес филиала: " + filial.Address);
            result.AppendLine("Адрес филиала на карте:" + filial.LocationUrl);
            result.AppendLine("Для связи с филиалом: ...");

            return result.ToString();
        }

        private static List<string> OrdersMessageUz(List<Order> orders, CancellationToken cancellationToken = default)
        {
            List<string> result = new();
            StringBuilder msg = new();
            foreach (Order order in orders)
            {
                msg.AppendLine("1. Menyular:");
                foreach (var menu in order.Menus)
                {
                    msg.AppendLine($"Nomi: {menu.Menu!.Name}");
                    msg.AppendLine($"Narxi: {menu.Menu.Price}");
                    msg.AppendLine($"Soni: {menu.Amount}");
                    msg.AppendLine();
                }
                msg.AppendLine($"Buyurtmaning umumiy narxi: {order.TotalSumms}");
                msg.AppendLine($"Filial nomi: {order.Filial!.Name}");
                msg.AppendLine($"Yetkazilgan manzil: {order.Address}");
                msg.AppendLine($"Geolokatsiya: {order.LocationUrl ?? "Lokatsiya kiritilmagan"}");
                msg.AppendLine($"Yetkazilgan vaqti: {order.DeliveredTime!.Value.ToString("dd-MM-yyyy HH:mm")}");
                msg.AppendLine(); msg.AppendLine();

                result.Add(msg.ToString());
            }

            return result;
        }

        private static List<string> OrdersMessageEn(List<Order> orders, CancellationToken cancellationToken = default)
        {
            List<string> result = new();
            StringBuilder msg = new();
            foreach (Order order in orders)
            {
                msg.AppendLine("1. Manus:");
                foreach (var menu in order.Menus)
                {
                    msg.AppendLine($"Name: {menu.Menu!.Name}");
                    msg.AppendLine($"Price: {menu.Menu.Price}");
                    msg.AppendLine($"Amount: {menu.Amount}");
                    msg.AppendLine();
                }
                msg.AppendLine($"Total price of orders: {order.TotalSumms}");
                msg.AppendLine($"Filial namei: {order.Filial!.Name}");
                msg.AppendLine($"Delivered address: {order.Address}");
                msg.AppendLine($"Location: {order.LocationUrl ?? "Lokatsiya kiritilmagan"}");
                msg.AppendLine($"Delivered time: {order.DeliveredTime!.Value.ToString("dd-MM-yyyy HH:mm")}");
                msg.AppendLine(); msg.AppendLine();

                result.Add(msg.ToString());
            }

            return result;
        }

        private static List<string> OrdersMessageRu(List<Order> orders, CancellationToken cancellationToken = default)
        {
            List<string> result = new();
            StringBuilder msg = new();
            foreach (Order order in orders)
            {
                msg.AppendLine("1. Manus:");
                foreach (var menu in order.Menus)
                {
                    msg.AppendLine($"Name: {menu.Menu!.Name}");
                    msg.AppendLine($"Price: {menu.Menu.Price}");
                    msg.AppendLine($"Amount: {menu.Amount}");
                    msg.AppendLine();
                }
                msg.AppendLine($"Total price of orders: {order.TotalSumms}");
                msg.AppendLine($"Filial namei: {order.Filial!.Name}");
                msg.AppendLine($"Delivered address: {order.Address}");
                msg.AppendLine($"Location: {order.LocationUrl ?? "Lokatsiya kiritilmagan"}");
                msg.AppendLine($"Delivered time: {order.DeliveredTime!.Value.ToString("dd-MM-yyyy HH:mm")}");
                msg.AppendLine(); msg.AppendLine();

                result.Add(msg.ToString());
            }

            return result;
        }

    }
}
