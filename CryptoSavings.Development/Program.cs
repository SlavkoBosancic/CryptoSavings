using CryptoSavings.Contracts.DAL;
using CryptoSavings.DAL.HttpAPI;
using CryptoSavings.DAL.HttpClient;
using CryptoSavings.Model.DAL.HttpAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoSavings.Development
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var client = new RestSharpHttpClient();
            IHttpAPI cc = new CryptoCompareHttpAPI(client);

            var testlist = new List<Currency>();
            testlist.Add(new FiatCurrency() { Id = "USD", Name = "US Dollar", Symbol = "$" });
            testlist.Add(new FiatCurrency() { Id = "EUR", Name = "EURO", Symbol = "€" });
            testlist.Add(new CryptoCurrency() { Id = "BTC", Name = "Bitcoin" });
            testlist.Add(new CryptoCurrency() { Id = "XRP", Name = "Ripple" });

            var coins = cc.GetAllCryptoCurrencies();
            var aggregatedList = coins.Select(x => x as Currency).ToList();
            aggregatedList.Add(testlist[0]);
            aggregatedList.Add(testlist[1]);

            var exchanges = cc.GetAllExchanges(aggregatedList);

            var price = cc.CurrentTradePrice(testlist[0], testlist[2]);
            var prices = cc.CurrentTradePrices(testlist.GetRange(0, 1), testlist.GetRange(2, 1));

        }
    }
}