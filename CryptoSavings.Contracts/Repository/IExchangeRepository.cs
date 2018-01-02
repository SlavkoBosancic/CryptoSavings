using CryptoSavings.Model;
using System.Collections.Generic;

namespace CryptoSavings.Contracts.Repository
{
    public interface IExchangeRepository : IRepository<Exchange>
    {
        bool PopulateExchangeMarkets(IEnumerable<Exchange> exchanges);
    }
}
