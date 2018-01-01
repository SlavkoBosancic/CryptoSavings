using CryptoSavings.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.Infrastructure.Contracts.Core
{
    public interface IPortfolioManager
    {
        IEnumerable<Purchase> GetUserPortfoilo(User user);
    }
}
