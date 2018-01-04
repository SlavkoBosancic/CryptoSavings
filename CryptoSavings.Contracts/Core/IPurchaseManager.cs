using CryptoSavings.Model;
using System;
using System.Collections.Generic;

namespace CryptoSavings.Contracts.Core
{
    public interface IPurchaseManager
    {
        bool CreatePurchase(string fromCurrencyId, string toCurrencyId,
                            DateTime when, decimal price, double quantity,
                            string exchangeId, string userId);

        bool UpdatePurchase(Purchase purchase);
        bool DeletePurchase(int id);

        IEnumerable<Purchase> GetAllPurchases(string userId);
        Purchase GetSnglePurchase(int id);
    }
}
