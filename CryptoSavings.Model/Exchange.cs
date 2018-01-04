using CryptoSavings.Model.DAL.HttpAPI;
using CryptoSavings.Model.DAL.Repository;
using System.Collections.Generic;

namespace CryptoSavings.Model
{
    public class Exchange
    {
        [PrimaryKey(AutoAssigned = false)]
        public string Name { get; set; }
        public IDictionary<Currency, IEnumerable<Currency>> TradePairs { get; }

        public Exchange()
        {
            TradePairs = new Dictionary<Currency, IEnumerable<Currency>>();
        }
    }
}
