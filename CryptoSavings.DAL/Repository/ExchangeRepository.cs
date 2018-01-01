using CryptoSavings.Contracts.Repository;
using CryptoSavings.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.DAL.Repository
{
    public class ExchangeRepository : LiteDBRepository<Exchange>, IExchangeRepository
    {
        public int GetExchangeMarketsCount()
        {
            return CountAll();
        }

        public bool PopulateExchangeMarkets(IEnumerable<Exchange> currencies)
        {
            throw new NotImplementedException();
        }
    }
}
