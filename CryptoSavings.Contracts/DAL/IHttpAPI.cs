using CryptoSavings.Model.DAL;
using CryptoSavings.Model.DAL.HttpAPI;
using System.Collections.Generic;

namespace CryptoSavings.Contracts.DAL
{
    public interface IHttpAPI
    {
        string APIName { get; }
        string APIHomePage { get; }

        IEnumerable<CryptoCurrency> GetAllCoins();
        TradePrice CurrentTradePrice(Currency fromCurrency, Currency toCurrency);
        IEnumerable<TradePrice> CurrentTradePrices(IEnumerable<Currency> fromCurrencies, IEnumerable<Currency> toCurrencies);
    }
}
