using CryptoSavings.Model.DAL.HttpAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.Model
{
    public class Exchange
    {
        public string Name { get; }
        public Dictionary<Currency, IEnumerable<Currency>> TradePairs { get; }

        public Exchange(string name)
        {
            Name = name;
            TradePairs = new Dictionary<Currency, IEnumerable<Currency>>();
        }
    }
}
