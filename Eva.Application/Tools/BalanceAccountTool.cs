using Eva.Infrastructure.InternalSerices.Balance;
using Eva.Infrastructure.InternalSerices.Balance.Models;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace Eva.Application.Tools
{
    public class BalanceAccountTool
    {
        private readonly IBalanceAccountService _saldoService;
        private readonly ILogger<SearchCardsTool> _logger;

        public BalanceAccountTool(IBalanceAccountService saldoService, ILogger<SearchCardsTool> logger)
        {
            _saldoService = saldoService;
            _logger = logger;
        }

        [Description("Faz a consulta de saldo do cartão através do número do documento do cliente.")]
        public async Task<ResponseBalance> GetBalanceAccountAsync(string cpf)
        {
            _logger.LogInformation("Iniciando consulta de saldo : {cpf}", cpf);
            var response = await _saldoService.GetSaldo(cpf);
            _logger.LogInformation("Consulta consulta de saldo concluída : {cardNumber}", cpf);
            return response;
        }
    }
}
