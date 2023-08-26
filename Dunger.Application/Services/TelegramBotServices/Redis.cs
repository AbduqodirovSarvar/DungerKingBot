using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace Dunger.Application.Services.TelegramBotServices
{
    public class Redis
    {
        private readonly IDatabase _redisDb;
        public Redis(IDatabase redis)
        {
            _redisDb = redis;
        }

        public static string Configuration = "RedisConnectionString";
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
    }
}
