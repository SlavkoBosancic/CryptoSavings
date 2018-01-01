using CryptoSavings.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.Infrastructure.Contracts.Repository
{
    public interface IPurchaseRepository
    {
        IEnumerable<Purchase> GetPurchasesByUser(User user);
    }
}
