using Refit;
using System.Text.Json;

namespace Eva.Infrastructure.ExternalServices.WhatsApp.WhatsAppApi
{
    public interface IWhatsAppApi
    {
        [Post("/{phoneNumberId}/messages")]
        Task<ApiResponse<JsonElement>> SendMessageAsync(
            [AliasAs("phoneNumberId")] string phoneNumberId,
            [Body] object payload,
            [Header("Authorization")] string authorization);
    }
}