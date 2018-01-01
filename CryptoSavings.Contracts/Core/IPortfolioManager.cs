using CryptoSavings.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.Contracts.Core
{
    public interface IPortfolioManager
    {
        IEnumerable<Purchase> GetUserPortfoilo(User user);
    }
}
