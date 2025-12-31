using Eva.Infrastructure.InternalSerices.Eligibility;
using Eva.Infrastructure.InternalSerices.Eligibility.Models;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace Eva.Application.Tools
{
    public class ElegibilityTool
    {
        private readonly IEligibilityService _eligibilityService;
        private readonly ILogger<ElegibilityTool> _logger;

        public ElegibilityTool(
            IEligibilityService eligibilityService,
            ILogger<ElegibilityTool> logger)
        {
            _eligibilityService = eligibilityService;
            _logger = logger;
        }

        [Description("Faz a consulta de elegibilidade através do número do documento do cliente.")]
        public async Task<HttpResponseMessage> GetEligibilityAsync(EligibilityRequest request)
        {
            _logger.LogInformation("Iniciando consulta de elegibilidade para o documento: {DocumentNumber}", request.DocumentNumber);
            var response = await _eligibilityService.GetEligibilityAsync(request);
            _logger.LogInformation("Consulta de elegibilidade concluída para o documento: {DocumentNumber}", request.DocumentNumber);
            return response;
        }
    }
}
