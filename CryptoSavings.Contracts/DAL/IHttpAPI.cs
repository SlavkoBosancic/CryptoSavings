using CryptoSavings.Model.DAL;
using CryptoSavings.Model.DAL.HttpAPI;
using System.Collections.Generic;

namespace CryptoSavings.Contracts.DAL
{
    public interface IHttpAPI
    {
        string APIName { get; }
        string APIHomePage { get; }

        IEnumerable<Coin> GetAllCoins();
        CoinPrice CurrentCoinPrice(Coin coin, Currency currency);
        IEnumerable<CoinPrice> CurrentCoinPrices(IEnumerable<Coin> coins, IEnumerable<Currency> currencies);
    }
}
