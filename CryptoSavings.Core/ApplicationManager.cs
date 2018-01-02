using CryptoSavings.Contracts.Core;
using CryptoSavings.Contracts.DAL;
using CryptoSavings.Contracts.Repository;
using CryptoSavings.Model;
using CryptoSavings.Model.DAL.HttpAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoSavings.Core
{
    public class ApplicationManager : IApplicationManager
    {
        private bool _dataInitialized;
        private static readonly object _dataInitializationLock = new object();

        private readonly IHttpAPI _httpAPI;

        private readonly ICryptoRepository _cryptoRepository;
        private readonly IFiatRepository _fiatRepository;
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IUserRepository _userRepository;

        #region [CTOR]

        public ApplicationManager(IHttpAPI httpAPI, IFiatRepository fiatRepository,
            ICryptoRepository cryptoRepository, IExchangeRepository exchangeRepository, IUserRepository userRepository)
        {
            _httpAPI = httpAPI ?? throw new ArgumentNullException(nameof(httpAPI));

            _fiatRepository = fiatRepository ?? throw new ArgumentNullException(nameof(fiatRepository));
            _cryptoRepository = cryptoRepository ?? throw new ArgumentNullException(nameof(cryptoRepository));
            _exchangeRepository = exchangeRepository ?? throw new ArgumentNullException(nameof(exchangeRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        #endregion

        public bool DataInitialized => _dataInitialized;

        public void DataInitialization()
        {
            if (!_dataInitialized)
            {
                lock (_dataInitializationLock)
                {
                    if (!_dataInitialized)
                    {
                        var result = true;
                        var fiatCurrencies = GetSupportedFiatCurrencies();
                        var cryptoCurrencies = new List<CryptoCurrency>();

                        // Handle Fiat currencies
                        var fiatPopulateSuccess = _fiatRepository.InsertOrUpdateFiatCurrencies(fiatCurrencies);
                        result = result && fiatPopulateSuccess;

                        // Handle crypto currencies
                        if (_cryptoRepository.CountAll() < 1)
                        {
                            if (!cryptoCurrencies.Any())
                            {
                                cryptoCurrencies.AddRange(_httpAPI.GetAllCryptoCurrencies());
                            }

                            var cryptoPopulateResult = _cryptoRepository.PopulateCryptoCurrencies(cryptoCurrencies);
                            result = result && cryptoPopulateResult;
                        }

                        // Handle exchange markets
                        if(_exchangeRepository.CountAll() < 1)
                        {
                            if (!cryptoCurrencies.Any())
                            {
                                cryptoCurrencies.AddRange(_httpAPI.GetAllCryptoCurrencies());
                            }

                            var mergedCurrencyList = cryptoCurrencies.Select(x => x as Currency).ToList();
                            mergedCurrencyList.AddRange(fiatCurrencies);

                            var exchanges = _httpAPI.GetAllExchanges(mergedCurrencyList);
                            var exchangePopulateResult = _exchangeRepository.PopulateExchangeMarkets(exchanges);

                            result = result && exchangePopulateResult;
                        }

                        // Add demo user
                        var demoUser = CreateDemoUser();
                        if(!_userRepository.Exists(x => x.Email == demoUser.Email))
                        {
                            var key = _userRepository.Create(demoUser);
                            result = result && (demoUser.Email == (string)key);
                        }

                        _dataInitialized = result;
                    }
                }
            }
        }

        #region [Private]

        private IEnumerable<FiatCurrency> GetSupportedFiatCurrencies()
        {
            var result = new List<FiatCurrency>();

            result.Add(new FiatCurrency { Id = "USD", Name = "US Dollar", Symbol = "$"});
            result.Add(new FiatCurrency { Id = "EUR", Name = "Euro", Symbol = "€" });
            result.Add(new FiatCurrency { Id = "GBP", Name = "British Pound", Symbol = "£" });

            return result;
        }

        private User CreateDemoUser()
        {
            return new User
            {
                Email = "demo@cryptosavings.com",
                FirstName = "Demo",
                LastName = "User"
            };
        }

        #endregion
    }
}
