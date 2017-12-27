using CryptoSavings.Model.DAL.HttpAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.Model.DAL
{
    public class TradePrice
    {
        public Currency FromCurrency { get; set; }
        public Currency ToCurrency { get; set; }
        public DateTime TimeStampUTC { get; set; }

        public double Price { get; set; }
    }
}
