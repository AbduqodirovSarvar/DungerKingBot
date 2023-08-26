using Dunger.Application.Services.TelegramBotServices;

namespace Dunger.Application.Services.TelegramBotStates
{
    public class RegisterState
    {
        private readonly Redis _redis;
        public RegisterState(Redis redis)
        {
            _redis = redis;
        }
        public static string[] States { get; set; } = new[] { "Language", "FirstName", "LastName", "Contact" };

        public async Task Next(long id)
        {
            var value = await _redis.GetUserState(id);
            int index = Array.IndexOf(States, value);

            if (States.Length - 1 == index)
            {
                await _redis.DeleteState(id);
            }
            else
            {
                await _redis.SetUserState(id, States[index + 1]);
            }
        }

        public async Task Finished(long Id)
        {
            await _redis.DeleteState(Id);
        }
    }
}
