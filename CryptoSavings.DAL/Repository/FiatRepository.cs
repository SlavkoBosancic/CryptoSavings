using CryptoSavings.Infrastructure.Contracts.Repository;
using CryptoSavings.Model.DAL.HttpAPI;
using System;
using System.Collections.Generic;

namespace CryptoSavings.DAL.Repository
{
    public class FiatRepository : LiteDBRepository<FiatCurrency>, IFiatRepository
    {
        public bool InsertOrUpdateFiatCurrencies(IEnumerable<FiatCurrency> currencies)
        {
            throw new NotImplementedException();
        }
    }
}
