using CryptoSavings.Model.DAL.HttpAPI;
using System.Collections.Generic;

namespace CryptoSavings.Infrastructure.Contracts.Repository
{
    public interface ICryptoRepository
    {
        int GetCryptoCurrencyCount();
        bool PopulateCryptoCurrencies(IEnumerable<CryptoCurrency> currencies);
    }
}
