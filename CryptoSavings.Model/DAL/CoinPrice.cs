using CryptoSavings.Model.DAL.HttpAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.Model.DAL
{
    public class CoinPrice
    {
        public Coin Coin { get; set; }
        public Currency Currency { get; set; }
        public decimal Price { get; set; }
    }
}
