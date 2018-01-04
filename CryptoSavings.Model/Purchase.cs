using CryptoSavings.Model.DAL.HttpAPI;
using System;
using System.Collections.Generic;

namespace CryptoSavings.Model
{
    public class Purchase
    {
        public int Id { get; set; }
        public Currency FromCurrency { get; set; }
        public Currency ToCurrency { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public DateTime TimeStampUTC { get; set; }
        public Exchange Exchange { get; set; }
        public User User { get; set; }

        public IDictionary<Currency, decimal> PriceEstimations { get; set; }

        public Purchase()
        {
            PriceEstimations = new Dictionary<Currency, decimal>();
        }
    }
}
