using Eva.Infrastructure.InternalSerices.HTTP;
using Eva.Infrastructure.InternalSerices.SearchCard.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Eva.Infrastructure.InternalSerices.SearchCard
{
    public class SearchCardsService : ISearchCardsService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SearchCardsService> _logger;

        public SearchCardsService(
            IHttpService httpService,
            IConfiguration configuration,
            ILogger<SearchCardsService> logger)
        {
            _httpService = httpService;
            _configuration = configuration;
            _logger = logger;
        }

        public Task<ResponseSearchCard> SearchCardsAsync(RequestSearchCard request)
        {
            var URL_API = _configuration["SearchCardsService:FullUrl"] ?? "";
            var retorno = _httpService.PostAsync<ResponseSearchCard>(URL_API, JsonSerializer.Serialize(request));

            _logger.LogInformation("SearchCardsService: {URL} {Request}",
                URL_API,
                JsonSerializer.Serialize(request));

            return retorno;
        }
    }
}
