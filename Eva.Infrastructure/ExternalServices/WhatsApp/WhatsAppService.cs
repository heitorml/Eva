using Eva.Infrastructure.ExternalServices.WhatsApp.WhatsAppApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Eva.Infrastructure.ExternalServices.WhatsApp
{
    public class WhatsAppService : IWhatsAppService
    {

        private readonly IWhatsAppApi _refitClient;
        private readonly string _accessToken;
        private readonly string _phoneNumberId;
        private readonly ILogger<WhatsAppService>? _logger;

        public WhatsAppService(
            IWhatsAppApi refitClient,
            IConfiguration configuration,
            ILogger<WhatsAppService>? logger = null)
        {
            _refitClient = refitClient ?? throw new ArgumentNullException(nameof(refitClient));
            _accessToken = configuration["WhatsApp:AccessToken"] ?? throw new ArgumentNullException("WhatsApp:AccessToken");
            _phoneNumberId = configuration["WhatsApp:PhoneNumberId"] ?? throw new ArgumentNullException("WhatsApp:PhoneNumberId");
            _logger = logger;
        }

        /// <summary>
        /// Envia uma mensagem de texto para um usuário do WhatsApp usando OAuth2 Bearer Token.
        /// </summary>
        /// <param name="to">Número do destinatário no formato internacional (ex: 5511999998888).</param>
        /// <param name="message">Mensagem de texto.</param>
        /// <returns>Retorna o ID da mensagem do provedor, se bem-sucedido.</returns>
        public async Task<string> SendMessageAsync(string to, string message)
        {
            if (string.IsNullOrWhiteSpace(to)) throw new ArgumentException("to is required", nameof(to));
            if (message is null) throw new ArgumentNullException(nameof(message));

            var payload = new
            {
                messaging_product = "whatsapp",
                to = to,
                type = "text",
                text = new { body = message }
            };

            var authHeader = $"Bearer {_accessToken}";

            var response = await _refitClient
                .SendMessageAsync(_phoneNumberId, payload, authHeader)
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var raw = response.Content.ValueKind != JsonValueKind.Undefined
                    ? response.Content.ToString()
                    : string.Empty;

                _logger?.LogError("WhatsApp API returned non-success. Status={Status} Body={Body}", response.StatusCode, raw);
                await response.EnsureSuccessStatusCodeAsync();
            }

            try
            {
                var root = response.Content;
                _logger?.LogWarning("WhatsApp response did not contain messages[0].id: {Response}", root.ToString());
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Failed to parse WhatsApp response");
                throw;
            }
        }

    }
}
