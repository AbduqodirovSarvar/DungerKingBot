namespace Dunger.Application.Abstractions.TelegramBotAbstractions
{
    public interface IOrderButtonServices
    {
        Task GetPreviousOrders(long chatId, List<string> messages, int languageId, CancellationToken cancellationToken = default);
        Task GetNextOrders(long chatId, List<string> messages, int languageId, CancellationToken cancellationToken = default);
    }
}
