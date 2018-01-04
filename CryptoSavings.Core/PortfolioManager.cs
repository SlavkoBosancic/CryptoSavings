using CryptoSavings.Contracts.Core;
using System;
using System.Collections.Generic;
using System.Text;
using CryptoSavings.Model;
using CryptoSavings.Contracts.Repository;

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
            return _purchaseRepository.GetPurchasesByUser(user.Email);
        }
    }
}
