using CryptoSavings.Contracts.Repository;
using CryptoSavings.Model.DAL.HttpAPI;
using System.Collections.Generic;
using System.Linq;

namespace CryptoSavings.DAL.Repository
{
    internal class FiatRepository : LiteDBRepository<FiatCurrency>, IFiatRepository
    {
        public bool InsertOrUpdateFiatCurrencies(IEnumerable<FiatCurrency> currencies)
        {
            var result = false;

            if(currencies != null && currencies.Any())
            {
                _db.GetCollection<FiatCurrency>()
                   .Upsert(currencies);

                result = CountAll() == currencies.Count();
            }

            return result;
        }
    }
}
