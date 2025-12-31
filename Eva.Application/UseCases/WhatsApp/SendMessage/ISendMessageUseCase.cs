namespace Eva.Application.UseCases.WhatsApp.SendMessage
{
    public interface ISendMessageUseCase
    {
        Task<object> SendMessageAsync(string userMessage);
    }
}
