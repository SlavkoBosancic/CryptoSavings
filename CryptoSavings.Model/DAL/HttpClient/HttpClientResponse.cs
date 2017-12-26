using System;
using System.Net;

namespace CryptoSavings.Model.DAL.HttpClient
{
    public class HttpClientResponse
    {
        public HttpClientResponse()
        {
            Status = ResponseStatus.Created;
            RawData = string.Empty;
            StatusCode = 0;
        }

        public ResponseStatus Status { get; set; }
        public string RawData { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public Exception ErrorException { get; set; }
    }

    public class HttpClientResponse<T> : HttpClientResponse
    {
        public T Data { get; set; }
    }
}
