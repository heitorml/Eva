namespace Eva.Application.UseCases.WhatsApp.ReceiveMessage
{
    public interface IReceiveMessageUseCase
    {
        Task<object> ReceiveMessageAsync(string userMessage);
    }
}
