using CryptoSavings.Model.DAL.HttpAPI;
using System;

namespace CryptoSavings.Model
{
    public class TradePrice
    {
        public Currency FromCurrency { get; set; }
        public Currency ToCurrency { get; set; }
        public DateTime TimeStampUTC { get; set; }

        public decimal Price { get; set; }
    }
}
