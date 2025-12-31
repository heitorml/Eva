using Eva.Infrastructure.InternalSerices.Accreditation;
using Eva.Infrastructure.InternalSerices.Accreditation.Models;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace Eva.Application.Tools
{
    public class AccreditationTool
    {
        private readonly IAccreditationService _service;
        private readonly ILogger<AccreditationTool> _logger;

        public AccreditationTool(IAccreditationService service, ILogger<AccreditationTool> logger)
        {
            _service = service;
            _logger = logger;
        }

        [Description("Faz o credenciamento do cliente.")]
        public async Task<HttpResponseMessage> AccreditationAsync(RequestAccreditation request)
        {
            _logger.LogInformation("Iniciando requisição de habilitação para credenciamento.");
            var response = await _service.Request(request);
            _logger.LogInformation("Requisição de habilitação para credenciamento concluída.");
            return response;
        }
    }
}

