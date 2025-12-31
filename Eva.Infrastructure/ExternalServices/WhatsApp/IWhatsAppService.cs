namespace Eva.Infrastructure.ExternalServices.WhatsApp
{
    public interface IWhatsAppService
    {
        Task<string> SendMessageAsync(string to, string message);
    }
}
