using Eva.Infrastructure.InternalSerices.HTTP;
using Eva.Infrastructure.InternalSerices.Reissue.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Eva.Infrastructure.InternalSerices.Reissue
{
    public class ReissueService : IReissueService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ReissueService> _logger;

        public ReissueService(
            IHttpService httpService,
            IConfiguration configuration,
            ILogger<ReissueService> logger)
        {
            this._httpService = httpService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> Confirm(string id)
        {
            var URL_API = _configuration["ReissueService:FullUrl"] ?? "";
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "token",  _configuration["ReissueService:Token"] ?? "" }
            };
            URL_API = $"{URL_API}/{id}";

            _logger.LogInformation(@$"ReissueService - Confirm - ROTE URL: {URL_API}");
            _logger.LogInformation(@$"ReissueService - Confirm - REQUEST HEADER: {JsonSerializer.Serialize(headers)}");

            return await _httpService.PostAsync<string>(URL_API, null, headers);
        }

        public async Task<string> Request(RequestReissue solicitacao)
        {
            var URL_API = _configuration["ReemissaoService:FullUrl"] ?? "";

            var queryParams = new Dictionary<string, string?>
            {
                { "cpf", solicitacao.Cpf },
                { "matricula", solicitacao.Matricula == null ? string.Empty : solicitacao.Matricula },
                { "nome", solicitacao.Nome },
                { "dt_nascimento", solicitacao.DataNascimento },
                { "motivo", solicitacao.Motivo }
            };

            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "unique_id", "1" },
                { "product", solicitacao.Produto },
                { "origin_request", "IA" },
                { "card_number", solicitacao.NumeroCartao },
                { "token",  _configuration["ReemissaoService:Token"] ?? "" }
            };


            Console.WriteLine($"URL: {URL_API}");
            _logger.LogInformation(@$"ReemissaoService - Request - ROTA URL: {URL_API}");
            _logger.LogInformation(@$"ReemissaoService - Request - REQUEST HEADER: {JsonSerializer.Serialize(headers)}");
            _logger.LogInformation(@$"ReemissaoService - Request - REQUEST PAYLOAD: {JsonSerializer.Serialize(queryParams)}");

            var retorno = await _httpService.GetAsync<ResponseReissue>(URL_API, queryParams, headers);

            string[] parts = retorno.Links.FirstOrDefault().href.Split('/');

            string id = parts[^1];

            Console.WriteLine($"ID Extraído: {id}");

            _logger.LogInformation("ReemissaoService: {Headers} {URL} {Request} {queryParams}",
                JsonSerializer.Serialize(headers),
                URL_API,
                JsonSerializer.Serialize(solicitacao),
                JsonSerializer.Serialize(queryParams));


            return id;
        }
    }
}
