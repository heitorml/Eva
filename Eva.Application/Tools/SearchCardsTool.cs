using Eva.Infrastructure.InternalSerices.SearchCard;
using Eva.Infrastructure.InternalSerices.SearchCard.Models;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace Eva.Application.Tools
{
    public class SearchCardsTool
    {
        private readonly ISearchCardsService _consultaCartaoApi;
        private readonly ILogger<SearchCardsTool> _logger;

        public SearchCardsTool(
            ISearchCardsService consultaCartaoApi,
            ILogger<SearchCardsTool> logger)
        {
            _consultaCartaoApi = consultaCartaoApi;
            _logger = logger;
        }

        [Description("Obtém dados do cartão, contrato e usuário. O cartão é um produto.")]
        public async Task<ResponseSearchCard> GetCardAync(RequestSearchCard request)
        {
            _logger.LogInformation("Iniciando consulta de cartão para CPF: {Cpf}", request.cpf);
            var response = await _consultaCartaoApi.SearchCardsAsync(request);
            _logger.LogInformation("Requisição de consulta de cartão: {@Request}", request);
            return response;
        }
    }
}
