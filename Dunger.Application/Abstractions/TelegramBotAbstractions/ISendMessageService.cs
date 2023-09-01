namespace Dunger.Application.Abstractions.TelegramBotAbstractions
{
    public interface ISendMessageService
    {
        Task SendMessageToAdmins(string message);
    }
}
