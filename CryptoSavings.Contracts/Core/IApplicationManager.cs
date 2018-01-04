using CryptoSavings.Model;

namespace CryptoSavings.Contracts.Core
{
    public interface IApplicationManager
    {
        bool DataInitialized { get; }
        void DataInitialization();
        User GetDemoUser();
    }
}
