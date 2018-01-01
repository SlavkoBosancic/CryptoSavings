using CryptoSavings.Model.DAL.HttpAPI;
using System.Collections.Generic;

namespace CryptoSavings.Infrastructure.Contracts.Repository
{
    public interface IFiatRepository
    {
        bool InsertOrUpdateFiatCurrencies(IEnumerable<FiatCurrency> currencies);
    }
}
