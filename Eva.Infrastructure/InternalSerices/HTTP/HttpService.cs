using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace Eva.Infrastructure.InternalSerices.HTTP
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly CookieContainer _cookieContainer;

        public HttpService(HttpClient httpService, IHttpMessageHandlerFactory handlerFactory)
        {
            _httpClient = httpService;
            var handler = handlerFactory.CreateHandler() as HttpClientHandler;
            _cookieContainer = handler?.CookieContainer ?? new CookieContainer();
        }
        public async Task<T?> GetAsync<T>(string baseUrl, Dictionary<string, string?>? queryParams = null, Dictionary<string, string>? headers = null)
        {
            // Construir a URL com os parâmetros opcionais
            var query = queryParams is not null
                ? string.Join("&", queryParams
                    .Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value!)}"))
                : string.Empty;

            var url = string.IsNullOrWhiteSpace(query) ? baseUrl : $"{baseUrl}?{query}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            AddHeaders(request, headers);

            var response = await _httpClient.SendAsync(request);
            var stringResponse = response.Content.ReadAsStringAsync().Result;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }


        public async Task<T?> PostAsync<T>(string url, string data, Dictionary<string, string>? headers = null, Dictionary<string, string?>? queryParams = null)
        {
            var query = queryParams is not null
               ? string.Join("&", queryParams
                   .Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value!)}"))
            : string.Empty;
            url = string.IsNullOrWhiteSpace(query) ? url : $"{url}?{query}";

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = data is not null ? new StringContent(data, Encoding.UTF8, "application/json") : null
            };


            AddHeaders(request, headers);
            var response = await _httpClient.SendAsync(request);
            var teste = response.Content.ReadAsStringAsync().Result;

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<HttpResponseMessage?> PostAsync(string url, string data, Dictionary<string, string>? headers = null, Dictionary<string, string?>? queryParams = null)
        {
            var query = queryParams is not null
               ? string.Join("&", queryParams
                   .Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value!)}"))
            : string.Empty;
            url = string.IsNullOrWhiteSpace(query) ? url : $"{url}?{query}";

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = data is not null ? new StringContent(data, Encoding.UTF8, "application/json") : null
            };


            AddHeaders(request, headers);
            var response = await _httpClient.SendAsync(request);
            return response;
        }

        private static void AddHeaders(HttpRequestMessage request, Dictionary<string, string>? headers)
        {
            request.Headers.TryAddWithoutValidation("content-type", "application/json;");
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    if (!string.IsNullOrEmpty(header.Value))
                    {
                        request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                    }
                }
            }
        }
    }
}
