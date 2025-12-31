using Eva.Infrastructure.InternalSerices.Reissue;
using Eva.Infrastructure.InternalSerices.Reissue.Models;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace Eva.Application.Tools
{
    public class ReissueTool
    {
        private readonly IReissueService _reemissaoService;
        private readonly ILogger<ReissueTool> _logger;

        public ReissueTool(IReissueService reemissaoService, ILogger<ReissueTool> logger)
        {
            _reemissaoService = reemissaoService;
            _logger = logger;
        }

        [Description("Solicita uma reemissão do cartão.")]
        public async Task<string> RequestReissueAsync(
            string cpf,
            string? matricula,
            string nome,
            string dataNascimento,
            string motivo)
        {
            try
            {
                var solicitacao = new RequestReissue
                {
                    Cpf = cpf,
                    Matricula = matricula,
                    Nome = nome,
                    DataNascimento = dataNascimento,
                    Motivo = motivo
                };
                _logger.LogInformation("Iniciando solicitação de reemissão de cartão para CPF: {Cpf}", cpf);
                var resposta = await _reemissaoService.Request(solicitacao);
                _logger.LogInformation("Solicitação de reemissão de cartão concluída para CPF: {Cpf}", cpf);
                return resposta;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao solicitar reemissão de cartão para CPF: {Cpf}", cpf);
                throw;
            }
        }
    }
}
