using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.Model
{
    public class Purchase
    {
        public int Id { get; set; }
        public TradePrice TradePrice { get; set; }
        public double Quantity { get; set; }
        public Exchange Exchange { get; set; }
        public User User { get; set; }
    }
}
