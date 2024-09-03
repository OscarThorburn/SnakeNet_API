namespace SnakeNet_API.Services.Interfaces
{
    public interface IMessageQueueService
    {
        Task SendMessageAsync(string message, string title);
    }
}