using CryptoSavings.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.Contracts.Repository
{
    public interface IPurchaseRepository
    {
        IEnumerable<Purchase> GetPurchasesByUser(User user);
    }
}
