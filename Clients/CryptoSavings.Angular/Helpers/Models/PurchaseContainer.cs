using System;

namespace CryptoSavings.Angular.Helpers.Models
{
    public class PurchaseContainer
    {
        public string FromCurrencyId { get; set; }
        public string ToCurrencyId { get; set; }
        public DateTime When { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public string ExchangeId { get; set; }
    }
}
