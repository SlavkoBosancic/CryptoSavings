﻿using CryptoSavings.Contracts.DAL;
using CryptoSavings.Model.DAL;
using CryptoSavings.Model.DAL.HttpAPI;
using CryptoSavings.Model.DAL.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoSavings.DAL.HttpAPI
{
    public class CryptoCompareHttpAPI : IHttpAPI
    {
        private readonly IHttpClient _httpClient;
        private readonly string _baseUrl = "https://min-api.cryptocompare.com/data/";
        private readonly string _oldBaseUri = "https://www.cryptocompare.com/api/data/";

        #region [CTOR]

        public CryptoCompareHttpAPI(IHttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        #endregion

        public string APIName => "Crypto Compare";
        public string APIHomePage => "https://www.cryptocompare.com";

        public IEnumerable<CryptoCurrency> GetAllCoins()
        {
            var result = new List<CryptoCurrency>();
            var fullUrl = _baseUrl + "all/coinlist";        // https://min-api.cryptocompare.com/data/all/coinlist

            var request = _httpClient.CreateRequest(fullUrl);
            var response = _httpClient.ExecuteRequest<Dictionary<string, object>>(request);

            if (CheckAPIResponse(response))
            {
                if(response.Data.ContainsKey("Data") && response.Data["Data"] is Dictionary<string, object>)
                {
                    var baseDetailsUrl = response.Data.ContainsKey("BaseLinkUrl") ? response.Data["BaseLinkUrl"] : string.Empty;
                    var baseImgUrl = response.Data.ContainsKey("BaseImageUrl") ? response.Data["BaseImageUrl"] : string.Empty;
                    

                    var coins = response.Data["Data"] as Dictionary<string, object>;
                    foreach(var coin in coins)
                    {
                        if(coin.Value is Dictionary<string, object>)
                        {
                            var coinCast = coin.Value as Dictionary<string, object>;
                            result.Add(new CryptoCurrency
                            {
                                Id = coinCast["Symbol"] as string,
                                Name = coinCast["CoinName"] as string,

                                DetailsUrl = coinCast.ContainsKey("Url") ? string.Format("{0}{1}", baseDetailsUrl, coinCast["Url"]) : string.Empty,
                                ImageUrl = coinCast.ContainsKey("ImageUrl") ? string.Format("{0}{1}", baseImgUrl, coinCast["ImageUrl"]) : string.Empty
                            });
                        }
                    }
                }
            }

            return result;
        }

        public TradePrice CurrentTradePrice(Currency fromCurrency, Currency toCurrency)
        {
            TradePrice result = null;
            var fullUrl = _baseUrl + "price";        // https://min-api.cryptocompare.com/data/price?fsym=BTC&tsyms=USD

            if (!fromCurrency.IsEmpty && !toCurrency.IsEmpty)
            {
                var request = _httpClient.CreateRequest(fullUrl);
                request.Parameters.Add(new HttpParameter { Key = "fsym", Value = fromCurrency.Id, Type = HttpParameterType.QUERY });
                request.Parameters.Add(new HttpParameter { Key = "tsyms", Value = toCurrency.Id, Type = HttpParameterType.QUERY });

                var response = _httpClient.ExecuteRequest<Dictionary<string, object>>(request);
                if (CheckAPIResponse(response))
                {
                    if (response.Data.ContainsKey(toCurrency.Id))
                    {
                        result = new TradePrice()
                        {
                            FromCurrency = fromCurrency,
                            ToCurrency = toCurrency,
                            TimeStampUTC = DateTime.UtcNow,
                            Price = Convert.ToDouble(response.Data[toCurrency.Id])
                        };
                    }
                }
            }

            return result;
        }

        public IEnumerable<TradePrice> CurrentTradePrices(IEnumerable<Currency> fromCurrencies, IEnumerable<Currency> toCurrencies)
        {
            var result = new List<TradePrice>();
            var fullUrl = _baseUrl + "pricemulti";        // https://min-api.cryptocompare.com/data/pricemulti?fsyms=BTC,ETH&tsyms=USD,EUR

            if (fromCurrencies.Any() && toCurrencies.Any())
            {
                var fromCurrencyIds = fromCurrencies.Where(x => !x.IsEmpty).Select(x => x.Id);
                var toCurrencyIds = toCurrencies.Where(x => !x.IsEmpty).Select(x => x.Id);

                if (fromCurrencyIds.Any() && toCurrencyIds.Any())
                {
                    var request = _httpClient.CreateRequest(fullUrl);
                    request.Parameters.Add(new HttpParameter { Key = "fsyms", Value = string.Join(",", fromCurrencyIds), Type = HttpParameterType.QUERY });
                    request.Parameters.Add(new HttpParameter { Key = "tsyms", Value = string.Join(",", toCurrencyIds), Type = HttpParameterType.QUERY });

                    var response = _httpClient.ExecuteRequest<Dictionary<string, object>>(request);
                    if (CheckAPIResponse(response))
                    {
                        //if (response.Data.ContainsKey(toCurrency.Id))
                        //{
                        //    result = new TradePrice()
                        //    {
                        //        FromCurrency = fromCurrency,
                        //        ToCurrency = toCurrency,
                        //        TimeStampUTC = DateTime.UtcNow,
                        //        Price = Convert.ToDouble(response.Data[toCurrency.Id])
                        //    };
                        //}
                    }
                }
            }

            return result;
        }

        #region [Private]

        private bool CheckAPIResponse(HttpClientResponse response)
        {
            var result = false;

            if (response.Status == ResponseStatus.Success)
            {
                result = true;

                if (response is HttpClientResponse<Dictionary<string, object>>)
                {
                    var data = ((HttpClientResponse<Dictionary<string, object>>)response).Data;

                    if (data.ContainsKey("Response"))
                    {
                        result = result && ((string)data["Response"]) == "Success";
                    }

                    if (data.ContainsKey("Type"))
                    {
                        result = result && (Convert.ToInt32(data["Type"]) >= 100);
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
