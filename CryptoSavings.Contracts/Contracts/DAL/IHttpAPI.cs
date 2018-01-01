using CryptoSavings.Model;
using CryptoSavings.Model.DAL.HttpAPI;
using System.Collections.Generic;

namespace CryptoSavings.Infrastructure.Contracts.DAL
{
    public interface IHttpAPI
    {
        string APIName { get; }
        string APIHomePage { get; }

        IEnumerable<CryptoCurrency> GetAllCryptoCurrencies();
        IEnumerable<Exchange> GetAllExchanges(IEnumerable<Currency> allCurrencies);

        TradePrice CurrentTradePrice(Currency fromCurrency, Currency toCurrency);
        IEnumerable<TradePrice> CurrentTradePrices(IEnumerable<Currency> fromCurrencies, IEnumerable<Currency> toCurrencies);
    }
}
