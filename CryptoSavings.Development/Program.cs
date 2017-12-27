using CryptoSavings.Contracts.DAL;
using CryptoSavings.DAL.HttpAPI;
using CryptoSavings.DAL.HttpClient;
using CryptoSavings.Model.DAL.HttpAPI;
using System;
using System.Collections.Generic;

namespace CryptoSavings.Development
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var client = new RestSharpHttpClient();
            IHttpAPI cc = new CryptoCompareHttpAPI(client);

            var coins = cc.GetAllCoins();

            var list = new List<Currency>();
            list.Add(new FiatCurrency() { Id = "USD", Name = "Dolla" });
            list.Add(new FiatCurrency() { Id = "EUR", Name = "Ojro" });
            list.Add(new CryptoCurrency() { Id = "BTC", Name = "Bitcoin" });
            list.Add(new CryptoCurrency() { Id = "XRP", Name = "Ripple" });

            var price = cc.CurrentTradePrice(list[2], list[0]);
            var prices = cc.CurrentTradePrices(list.GetRange(2, 2), list.GetRange(0, 2));
        }
    }
}