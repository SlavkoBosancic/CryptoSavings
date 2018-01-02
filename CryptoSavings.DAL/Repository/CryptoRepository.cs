using CryptoSavings.Contracts.Repository;
using CryptoSavings.Model.DAL.HttpAPI;
using System.Collections.Generic;
using System.Linq;

namespace CryptoSavings.DAL.Repository
{
    internal class CryptoRepository : LiteDBRepository<CryptoCurrency>, ICryptoRepository
    {
        public bool PopulateCryptoCurrencies(IEnumerable<CryptoCurrency> currencies)
        {
            var result = false;

            if (currencies != null && currencies.Any())
            {
                var insertCount = _db.GetCollection<CryptoCurrency>()
                                     .InsertBulk(currencies);

                result = currencies.Count() == insertCount;
            }

            return result;
        }
    }
}
