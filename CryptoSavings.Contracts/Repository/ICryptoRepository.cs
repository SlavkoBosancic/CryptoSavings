using CryptoSavings.Model.DAL.HttpAPI;
using System.Collections.Generic;

namespace CryptoSavings.Contracts.Repository
{
    public interface ICryptoRepository : IRepository<CryptoCurrency>
    {
        bool PopulateCryptoCurrencies(IEnumerable<CryptoCurrency> currencies);
    }
}
