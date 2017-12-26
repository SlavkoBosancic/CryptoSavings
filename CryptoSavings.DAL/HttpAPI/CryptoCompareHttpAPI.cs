using CryptoSavings.Contracts.DAL;
using CryptoSavings.DAL.Helpers;
using CryptoSavings.Model.DAL;
using CryptoSavings.Model.DAL.HttpAPI;
using CryptoSavings.Model.DAL.HttpClient;
using System;
using System.Collections.Generic;

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

        public IEnumerable<Coin> GetAllCoins()
        {
            var result = new List<Coin>();
            var fullUrl = _baseUrl + "all/coinlist";        // https://min-api.cryptocompare.com/data/all/coinlist

            var request = _httpClient.CreateRequest(fullUrl);
            var response = _httpClient.ExecuteRequest<CryptoCompareCoinList>(request);

            if (CheckAPIResponse(response))
            {
                if(response.Data.Data is Dictionary<string, dynamic>)
                {
                    var baseDetailsUrl = response.Data.BaseLinkUrl;
                    var baseImgUrl = response.Data.BaseImageUrl;
                    

                    var coins = response.Data.Data as Dictionary<string, dynamic>;
                    foreach(var coin in coins)
                    {
                        if(coin.Value is Dictionary<string, object>)
                        {
                            var coinCast = coin.Value as Dictionary<string, object>;
                            result.Add(new Coin
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

        public CoinPrice CurrentCoinPrice(Coin coin, Currency currency)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CoinPrice> CurrentCoinPrices(IEnumerable<Coin> coins, IEnumerable<Currency> currencies)
        {
            throw new NotImplementedException();
        }

        #region [Private]

        private bool CheckAPIResponse<T>(HttpClientResponse<T> response) where T : BaseCryptoCompareResponse
        {
            var result = false;

            if (response.Status == ResponseStatus.Success)
            {
                result = response.Data.Type >= 100 && response.Data.Response == "Success";
            }

            return result;
        }

        #endregion
    }
}
