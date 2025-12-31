namespace Eva.Infrastructure.InternalSerices.HTTP
{
    public interface IHttpService
    {
        Task<T?> GetAsync<T>(string baseUrl, Dictionary<string, string?>? queryParams = null, Dictionary<string, string>? headers = null);
        Task<T?> PostAsync<T>(string url, string data, Dictionary<string, string>? headers = null, Dictionary<string, string?>? queryParams = null);
        Task<HttpResponseMessage?> PostAsync(string url, string data, Dictionary<string, string>? headers = null, Dictionary<string, string?>? queryParams = null);
    }
}
