using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.Model
{
    public class Purchase
    {
        public TradePrice TradePrice { get; set; }
        public double Quantity { get; set; }
        public string ExchangeName { get; set; }
        public string UserEmail { get; set; }
    }
}
