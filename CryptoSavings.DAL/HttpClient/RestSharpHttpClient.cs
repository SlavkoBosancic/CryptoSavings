using CryptoSavings.Contracts.DAL;
using CryptoSavings.Model.DAL.HttpClient;
using RestSharp;
using System;
using System.Collections.Generic;
using HttpClientModels = CryptoSavings.Model.DAL.HttpClient;

namespace CryptoSavings.DAL.HttpClient
{
    public class RestSharpHttpClient : IHttpClient
    {
        private readonly IRestClient _client;

        #region [CTOR]

        public RestSharpHttpClient()
        {
            _client = new RestClient();
        }

        #endregion

        public HttpClientRequest CreateRequest(string url, HttpMethod httpMethod = HttpMethod.GET)
        {
            Uri uri;

            if(!Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uri))
                return null;

            return new HttpClientRequest
            {
                BaseUri = uri,
                HttpMethod = httpMethod
            };
        }

        public HttpClientResponse ExecuteRequest(HttpClientRequest request)
        {
            HttpClientResponse result = new HttpClientResponse();

            var restSharpRequest = PrepareRequest(request);
            if(restSharpRequest != null)
            {
                _client.BaseUrl = request.BaseUri;
                var restSharpResponse = _client.Execute(restSharpRequest);

                PrepareResponse(restSharpResponse, result);
            }
            else
            {
                result.ErrorException = new ArgumentNullException(nameof(restSharpRequest));
            }

            return result;
        }

        public HttpClientResponse<T> ExecuteRequest<T>(HttpClientRequest request) where T : new()
        {
            HttpClientResponse<T> result = new HttpClientResponse<T>();

            var restSharpRequest = PrepareRequest(request);
            if (restSharpRequest != null)
            {
                _client.BaseUrl = request.BaseUri;
                var restSharpResponse = _client.Execute<T>(restSharpRequest);

                PrepareResponse(restSharpResponse, result);
                result.Data = restSharpResponse.Data;
            }
            else
            {
                result.ErrorException = new ArgumentNullException(nameof(restSharpRequest));
            }

            return result;
        }

        #region [Private]

        private RestRequest PrepareRequest(HttpClientRequest request)
        {
            RestRequest result = null;

            if(request != null)
            {
                result = new RestRequest()
                {
                    Method = ConvertMethod(request.HttpMethod)
                };

                foreach (var parameter in request.Parameters)
                    result.Parameters.Add(ConvertParameter(parameter));
            }

            return result;
        }

        private void PrepareResponse(IRestResponse restSharpResponse, HttpClientResponse response)
        {
            if (restSharpResponse != null && response != null)
            {
                response.Status = restSharpResponse.ResponseStatus == RestSharp.ResponseStatus.Completed &&
                                   restSharpResponse.IsSuccessful ?
                                       HttpClientModels.ResponseStatus.Success :
                                       HttpClientModels.ResponseStatus.Error;

                response.StatusCode = restSharpResponse.StatusCode;
                response.ErrorException = restSharpResponse.ErrorException;
                response.RawData = restSharpResponse.Content;
            }
        }

        // For good encapsulation, these kind of conversion methods are needed
        private Method ConvertMethod(HttpClientModels.HttpMethod method)
        {
            switch (method)
            {
                case HttpMethod.GET:
                    return Method.GET;
                case HttpMethod.DELETE:
                    return Method.DELETE;
                case HttpMethod.HEAD:
                    return Method.HEAD;
                case HttpMethod.MERGE:
                    return Method.MERGE;
                case HttpMethod.OPTIONS:
                    return Method.OPTIONS;
                case HttpMethod.PATCH:
                    return Method.PATCH;
                case HttpMethod.POST:
                    return Method.POST;
                case HttpMethod.PUT:
                    return Method.PUT;
                default:
                    return Method.GET;
            }
        }

        private Parameter ConvertParameter(HttpClientModels.HttpParameter parameter)
        {
            Parameter result = null;

            if(parameter != null)
            {
                result = new Parameter()
                {
                    Value = parameter.Value,
                    Name = parameter.Key,
                    Type = ConvertParameterType(parameter.Type)
                };
            }

            return result;
        }

        private ParameterType ConvertParameterType(HttpClientModels.HttpParameterType parameterType)
        {
            switch (parameterType)
            {
                case HttpParameterType.BODY:
                    return ParameterType.RequestBody;
                case HttpParameterType.QUERY:
                    return ParameterType.QueryString;
                case HttpParameterType.COOKIE:
                    return ParameterType.Cookie;
                case HttpParameterType.HEADER:
                    return ParameterType.HttpHeader;
                default:
                    return ParameterType.QueryString;
            }
        }

        #endregion
    }
}
