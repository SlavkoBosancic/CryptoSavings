using CryptoSavings.DAL.HttpAPI;
using CryptoSavings.DAL.HttpClient;
using CryptoSavings.DAL.Repository;
using CryptoSavings.Infrastructure.Contracts.DAL;
using CryptoSavings.Model;
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

            TestUserRepository();
            TestPurchaseRepository(price, exchanges.ElementAt(0));
        }

        static void TestUserRepository()
        {
            var user = new User { Email = "kica@", FirstName = "kica", LastName = "kica" };
            var user1 = new User { Email = "kica1@", FirstName = "kica1", LastName = "kica1" };
            var user2 = new User { Email = "kica2@", FirstName = "kica2", LastName = "kica2" };
            var user3 = new User { Email = "kica3@", FirstName = "kica3", LastName = "kica3" };
            var user4 = new User { Email = "kica4@", FirstName = "kica4", LastName = "kica4" };
            var n = new LiteDBRepository<User>();

            var key = n.Create(user);
            var key1 = n.Create(user1);
            var key2 = n.Create(user2);
            var key3 = n.Create(user3);
            var key4 = n.Create(user4);

            var c = n.CountAll();
            var c1 = n.Count(x => x.Email == user.Email);
            var c2 = n.Count(x => x.Email.EndsWith("@"));
            var c3 = n.Count(x => x.Email.StartsWith("@"));

            var u = n.GetAll();
            var u1 = n.Get(x => x.Email == "kkk");
            var u2 = n.Get(x => x.Email == user.Email);

            var u3 = n.GetSingle(x => x.Email == "xxxxxx");
            var u4 = n.GetSingle(x => x.Email == user.Email);

            var e = n.Exists(x => x.Email == user.Email);

            user.FirstName = "Updated";
            var up = n.Update(user);
            var up1 = n.Update(new User { Email = "new user" });
            var up2 = n.GetSingle(x => x.Email == user.Email);
            var up3 = n.GetSingle(x => x.Email == "new user");


            var d = n.Delete(new User { Email = "new user" });
            var d0 = n.Delete(user);
            var d1 = n.Delete(user1);
            var d2 = n.Delete(user2);
            var d3 = n.Delete(user3);
            var d4 = n.Delete(user4);

            var c4 = n.CountAll();
        }

        static void TestPurchaseRepository(TradePrice tradePrice, Exchange exchange)
        {
            var ur = new LiteDBRepository<User>();
            var pu = new LiteDBRepository<Purchase>();

            var user = new User { Email = "slavko", FirstName = "test", LastName = "testovic" };
            var ukey = ur.Create(user);

            var pur = new Purchase
            {
                Exchange = exchange,
                TradePrice = tradePrice,
                Quantity = 1.023,
                User = user
            };

            var pur1 = new Purchase
            {
                Id = 777,
                Exchange = exchange,
                TradePrice = tradePrice,
                Quantity = 123,
                User = new User { Email = "slavko1", FirstName = "test1" }
            };

            var key = pu.Create(pur);
            var key1 = pu.Create(pur1);

            var c = pu.CountAll();
            var c1 = pu.Count(x => x.Id == (int)key);
            var c2 = pu.Count(x => x.Quantity == 123);
            var c3 = pu.Count(x => x.Exchange.Name == exchange.Name);

            var u = pu.GetAll();
            var u1 = pu.Get(x => x.Id == 777);
            var u2 = pu.Get(x => x.Exchange.Name == exchange.Name);

            var u3 = pu.GetSingle(x => x.Id == (int)key1);
            var u4 = pu.GetSingle(x => x.Exchange.Name == exchange.Name);

            var e = pu.Exists(x => x.Exchange.Name == exchange.Name);

            pur.Quantity = 0.00045;
            var up = pu.Update(pur);
            var up1 = pu.Update(new Purchase { Quantity = 123456789 });
            var up2 = pu.GetSingle(x => x.Id == (int)key);
            var up3 = pu.GetSingle(x => x.Quantity == 123456789);


            var d = pu.Delete(new Purchase { Id = 123456789 });
            var d0 = pu.Delete(pur);
            var d1 = pu.Delete(pur1);
            var d2 = ur.Delete(user);

            var c4 = pu.CountAll();
        }
    }
}