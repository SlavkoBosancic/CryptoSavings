using CryptoSavings.Model;
using System.Collections.Generic;

namespace CryptoSavings.Infrastructure.Contracts.Repository
{
    public interface IExchangeRepository
    {
        int GetExchangeMarketsCount();
        bool PopulateExchangeMarkets(IEnumerable<Exchange> currencies);
    }
}
