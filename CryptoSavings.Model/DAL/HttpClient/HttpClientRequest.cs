using System;
using System.Collections.Generic;

namespace CryptoSavings.Model.DAL.HttpClient
{
    public class HttpClientRequest
    {
        public HttpClientRequest()
        {
            Parameters = new List<HttpParameter>();
        }

        public Uri BaseUri { get; set; }
        public List<HttpParameter> Parameters { get; }
        public HttpMethod HttpMethod { get; set; }
    }
}
