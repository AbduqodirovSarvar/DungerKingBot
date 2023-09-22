using Dunger.Application.Services.TelegramServices.TelegramBotStates;
using StackExchange.Redis;

namespace Dunger.Application.Services.TelegramServices.TelegramBotServices
{
    public class Redis
    {
        /*private readonly IDatabase _redisDb;
        public Redis(IDatabase redis)
        {
            _redisDb = redis;
        }*/

        private static Dictionary<string, string> KeyValuesState = new();

        public static readonly string Configuration = "RedisConnectionString";

        public static Task SetUserState(long chatId, string state)
        {
            if (KeyValuesState.ContainsKey($"{chatId}"))
            {
                KeyValuesState[$"{chatId}"] = state;
                return Task.CompletedTask;
            }

            KeyValuesState.Add($"{chatId}", state);
            return Task.CompletedTask;
        }

        public static Task DeleteState(long Id)
        {
            if (KeyValuesState.ContainsKey($"{Id}"))
            {
                KeyValuesState.Remove($"{Id}");
            }

            return Task.CompletedTask;
        }

        public static string? GetUserState(long Id)
        {
            if (KeyValuesState.ContainsKey($"{Id}"))
            {
                return KeyValuesState[$"{Id}"];
            }
            return null;
        }

        public static Task Next(long Id)
        {
            string? state = KeyValuesState[$"{Id}"];
            Console.WriteLine(state ?? "null");
            if (state == null)
            {
                return Task.CompletedTask;
            }
            KeyValuesState[$"{Id}"] = State.states[Array.IndexOf(State.states, state) + 1];

            return Task.CompletedTask;
        }

        /*public async Task SetUserState(long chatId, string state)
        {
            await _redisDb.StringSetAsync($"{chatId}", state);
        }

        public async Task<string?> GetUserState(long chatId)
        {
            string? state = await _redisDb.StringGetAsync($"{chatId}");
            return state;
        }

        public Task DeleteState(long Id)
        {
            _redisDb.KeyDelete($"{Id}");

            return Task.CompletedTask;
        }

        public async Task Next(long Id)
        {
            string? state = await _redisDb.StringGetAsync($"{Id}");
            if (state == null)
            {
                return;
            }
            await _redisDb.StringSetAsync($"{Id}", State.states[Array.IndexOf(State.states, state) + 1]);

            var a = await _redisDb.StringGetAsync($"{Id}");

            Console.WriteLine("STATE:   _____________________" + a);

            return;
        }*/
    }
}
