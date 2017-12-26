using CryptoSavings.Contracts.DAL;
using CryptoSavings.DAL.HttpAPI;
using CryptoSavings.DAL.HttpClient;
using System;

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
        }
    }
}