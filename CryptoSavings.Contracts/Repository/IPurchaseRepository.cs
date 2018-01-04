using CryptoSavings.Model;
using System.Collections.Generic;

namespace CryptoSavings.Contracts.Repository
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
        IEnumerable<Purchase> GetPurchasesByUser(string userId, bool includeReferences = false);
    }
}
