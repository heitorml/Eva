using Eva.Infrastructure.InternalSerices.Balance.Models;
using Eva.Infrastructure.InternalSerices.HTTP;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Eva.Infrastructure.InternalSerices.Balance
{
    public class BalanceAccountService : IBalanceAccountService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<BalanceAccountService> _logger;
        public BalanceAccountService(
            IHttpService httpService,
            IConfiguration configuration,
            ILogger<BalanceAccountService> logger)
        {
            _httpService = httpService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ResponseBalance> GetSaldo(string cardNumber)
        {
            string URL_API = _configuration["BalanceAccountService:FullUrl"] ?? "";
            string TOKEN = _configuration["BalanceAccountService:Token"] ?? "";
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "card_number", cardNumber },
                { "token", TOKEN }
            };

            var retorno = await _httpService.PostAsync<ResponseBalance>(URL_API, string.Empty, headers);

            _logger.LogInformation("SaldoService: {Headers} {URL} {Request}",
                JsonSerializer.Serialize(headers),
                URL_API,
                cardNumber);

            return retorno;
        }
    }
}
