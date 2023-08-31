using Dunger.Application.Services.TelegramBotStates;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using Telegram.Bot.Types;

namespace Dunger.Application.Services.TelegramBotServices
{
    public class Redis
    {
        private readonly IDatabase _redisDb;
        public Redis(IDatabase redis)
        {
            _redisDb = redis;
        }

        public static readonly string Configuration = "RedisConnectionString";
        public async Task SetUserState(long chatId, string state)
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
            if (state== null)
            {
                return;
            }
            await _redisDb.StringSetAsync($"{Id}", State.states[Array.IndexOf(State.states, state) + 1]);

            var a = await _redisDb.StringGetAsync($"{Id}");

            Console.WriteLine("STATE:   _____________________" + a);

            return;
        }
    }
}
