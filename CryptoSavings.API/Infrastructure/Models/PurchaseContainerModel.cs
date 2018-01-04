using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoSavings.API.Infrastructure.Models
{
    public class PurchaseContainerModel
    {
        public string FromCurrencyId { get; set; }
        public string ToCurrencyId { get; set; }
        public DateTime When { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public string ExchangeId { get; set; }
    }
}
