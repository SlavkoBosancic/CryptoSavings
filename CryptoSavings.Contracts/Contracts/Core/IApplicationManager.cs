using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.Infrastructure.Contracts.Core
{
    public interface IApplicationManager
    {
        bool DataInitialized { get; }
        void DataInitialization();
    }
}
