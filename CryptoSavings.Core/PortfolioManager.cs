using CryptoSavings.Infrastructure.Contracts.Core;
using CryptoSavings.Infrastructure.Contracts.Repository;
using CryptoSavings.Model;
using System;
using System.Collections.Generic;

namespace CryptoSavings.Core
{
    public class PortfolioManager : IPortfolioManager
    {
        private readonly IPurchaseRepository _purchaseRepository;

        #region [CTOR]

        public PortfolioManager(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository ?? throw new ArgumentNullException(nameof(purchaseRepository));
        }

        #endregion

        public IEnumerable<Purchase> GetUserPortfoilo(User user)
        {
            return _purchaseRepository.GetPurchasesByUser(user);
        }
    }
}
