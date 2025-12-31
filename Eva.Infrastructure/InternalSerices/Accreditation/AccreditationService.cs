using Eva.Infrastructure.InternalSerices.Accreditation.Models;
using Eva.Infrastructure.InternalSerices.HTTP;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Eva.Infrastructure.InternalSerices.Accreditation
{
    public class AccreditationService : IAccreditationService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccreditationService> _logger;
        public AccreditationService(
            IHttpService httpService,
            IConfiguration configuration,
            ILogger<AccreditationService> logger)
        {
            this._httpService = httpService;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<HttpResponseMessage> Request(RequestAccreditation request)
        {
            var URL_API = _configuration["AccreditationService:FullUrl"];
            var username = _configuration["AccreditationService:User"];
            var password = _configuration["AccreditationService:Password"];
            var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
            var basicAuth = Convert.ToBase64String(byteArray);
            var json = JsonSerializer.Serialize(request);

            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization",  $"Basic {basicAuth}" }
            };

            _logger.LogInformation(@$"AccreditationService - ROTE URL: {URL_API}");
            _logger.LogInformation(@$"AccreditationService - REQUEST HEADER: {JsonSerializer.Serialize(headers)}");
            _logger.LogInformation(@$"AccreditationService - REQUEST PAYLOAD: {json}");


            var response = await _httpService.PostAsync(URL_API, json, headers);


            _logger.LogInformation("AccreditationService: {Headers} {URL} {Request}",
                JsonSerializer.Serialize(headers),
                URL_API,
                JsonSerializer.Serialize(request));

            return response;
        }
    }
}
