using StackExchange.Redis;

namespace Dunger.Application.Services.TelegramBotServices
{
    public class Redis
    {
        private readonly IDatabase redisDb;
        public Redis(IDatabase redis)
        {
            redisDb = redis;
        }

        public static string Configuration = "RedisConnectionString";
        public async Task SetUserState(long chatId, string state)
        {
            await redisDb.StringSetAsync($"{chatId}", state);
        }

        public async Task<string> GetUserState(long chatId)
        {
            string state = await redisDb.StringGetAsync($"{chatId}");
            return state;
        }

        public Task DeleteState(long Id)
        {
            redisDb.KeyDelete($"{Id}");

            return Task.CompletedTask;
        }
    }
}
