using CryptoSavings.Model;
using CryptoSavings.Model.DAL.HttpAPI;
using System.Collections.Generic;

namespace CryptoSavings.Contracts.DAL
{
    public interface IHttpAPI
    {
        string APIName { get; }
        string APIHomePage { get; }

        IEnumerable<CryptoCurrency> GetAllCryptoCurrencies();
        IEnumerable<Exchange> GetAllExchanges(IEnumerable<Currency> allCurrencies);

        TradePrice CurrentTradePrice(string fromCurrencyId, string toCurrencyId);
        IEnumerable<TradePrice> CurrentTradePrices(IEnumerable<string> fromCurrencyIds, IEnumerable<string> toCurrencyIds);
    }
}
