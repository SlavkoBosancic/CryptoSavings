using CryptoSavings.Contracts.Repository;
using CryptoSavings.Model.DAL.HttpAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.DAL.Repository
{
    public class CryptoRepository : LiteDBRepository<CryptoCurrency>, ICryptoRepository
    {
        public int GetCryptoCurrencyCount()
        {
            return CountAll();
        }

        public bool PopulateCryptoCurrencies(IEnumerable<CryptoCurrency> currencies)
        {
            throw new NotImplementedException();
        }
    }
}
