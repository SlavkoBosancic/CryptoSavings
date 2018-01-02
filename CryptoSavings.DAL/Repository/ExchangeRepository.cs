using CryptoSavings.Contracts.Repository;
using CryptoSavings.Model;
using System.Collections.Generic;
using System.Linq;

namespace CryptoSavings.DAL.Repository
{
    internal class ExchangeRepository : LiteDBRepository<Exchange>, IExchangeRepository
    {
        public bool PopulateExchangeMarkets(IEnumerable<Exchange> exchanges)
        {
            var result = false;

            if(exchanges != null && exchanges.Any())
            {
                var insertCount = _db.GetCollection<Exchange>()
                                     .InsertBulk(exchanges);

                result = exchanges.Count() == insertCount;
            }

            return result;
        }
    }
}
