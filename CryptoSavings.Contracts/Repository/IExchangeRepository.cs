using CryptoSavings.Model;
using System.Collections.Generic;

namespace CryptoSavings.Contracts.Repository
{
    public interface IExchangeRepository
    {
        int GetExchangeMarketsCount();
        bool PopulateExchangeMarkets(IEnumerable<Exchange> currencies);
    }
}
