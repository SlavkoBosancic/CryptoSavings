using CryptoSavings.Model.DAL.HttpClient;

namespace CryptoSavings.Contracts.DAL
{
    public interface IHttpClient
    {
        /// <summary>
        /// Create HTTP API request, and define it`s query/body/cookie/header parameters afterwards
        /// </summary>
        /// <param name="url">Url string of the API endpoint.</param>
        /// <param name="httpMethod">HTTP method to use. (GET/POST/PUT etc.)</param>
        /// <returns>
        /// Returns a HttpClientRequest object that is used for execution later on.
        /// Returns NULL if url string argument is not in properly formated.
        /// </returns>
        HttpClientRequest CreateRequest(string url, HttpMethod httpMethod = HttpMethod.GET);
        HttpClientResponse ExecuteRequest(HttpClientRequest request);
        HttpClientResponse<T> ExecuteRequest<T>(HttpClientRequest request) where T : new();
    }
}
