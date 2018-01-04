using CryptoSavings.Model.DAL.HttpAPI;
using System;

namespace CryptoSavings.Model
{
    public class TradePrice
    {
        public string FromCurrencyId { get; set; }
        public string ToCurrencyId { get; set; }
        public DateTime TimeStampUTC { get; set; }

        public decimal Price { get; set; }
    }
}
