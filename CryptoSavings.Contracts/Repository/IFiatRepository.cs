using CryptoSavings.Model.DAL.HttpAPI;
using System.Collections.Generic;

namespace CryptoSavings.Contracts.Repository
{
    public interface IFiatRepository : IRepository<FiatCurrency>
    {
        bool InsertOrUpdateFiatCurrencies(IEnumerable<FiatCurrency> currencies);
    }
}
