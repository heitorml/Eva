using Eva.Infrastructure.InternalSerices.Eligibility.Models;
using Eva.Infrastructure.InternalSerices.HTTP;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Eva.Infrastructure.InternalSerices.Eligibility
{
    public class EligibilityService : IEligibilityService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<EligibilityService> _logger;

        public EligibilityService(
            IHttpService httpService,
            IConfiguration configuration,
            ILogger<EligibilityService> logger)
        {
            _httpService = httpService;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<HttpResponseMessage> GetEligibilityAsync(EligibilityRequest request)
        {
            var URL_API = _configuration["EligibilityService:FullUrl"];
            var username = _configuration["EligibilityService:User"];
            var password = _configuration["EligibilityService:Password"];
            var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
            var basicAuth = Convert.ToBase64String(byteArray);
            var json = JsonSerializer.Serialize(request);


            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization",  $"Basic {basicAuth}" }
            };

            _logger.LogInformation(@$"Eligibility - EligibilityService ROTA: {URL_API}");
            _logger.LogInformation(@$"Eligibility - EligibilityService REQUEST PAYLOAD: {json}");

            var response = await _httpService.PostAsync(URL_API, json, headers);


            _logger.LogInformation(@$"EligibilityService:  {URL_API}  REQUEST: {json}  RESPONSE: {response}");

            return response;
        }
    }
}
