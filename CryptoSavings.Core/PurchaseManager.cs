using System;
using CryptoSavings.Contracts.Core;
using CryptoSavings.Model;
using System.Collections.Generic;
using CryptoSavings.Model.DAL.HttpAPI;
using CryptoSavings.Contracts.Repository;
using System.Linq;
using CryptoSavings.Contracts.DAL;

namespace CryptoSavings.Core
{
    public class PurchaseManager : IPurchaseManager
    {
        // Use private GetMergedCurrencyList() method to generate and/or retrive items from this list
        private static readonly List<Currency> _mergedCurrencyList = new List<Currency>();
        private static readonly object _mergedCurrencyListLock = new object();

        private readonly IFiatRepository _fiatRepository;
        private readonly ICryptoRepository _cryptoRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IHttpAPI _httpAPI;

        #region [CTOR]

        public PurchaseManager(IFiatRepository fiatRepository, ICryptoRepository cryptoRepository,
                               IPurchaseRepository purchaseRepository, IHttpAPI httpAPI)
        {
            _fiatRepository = fiatRepository ?? throw new ArgumentNullException(nameof(fiatRepository));
            _cryptoRepository = cryptoRepository ?? throw new ArgumentNullException(nameof(cryptoRepository));
            _purchaseRepository = purchaseRepository ?? throw new ArgumentNullException(nameof(purchaseRepository));
            _httpAPI = httpAPI ?? throw new ArgumentNullException(nameof(httpAPI));
        }

        #endregion

        public bool CreatePurchase(string fromCurrencyId, string toCurrencyId,
                                         DateTime when, decimal price, double quantity,
                                         string exchangeId, string userId)
        {
            var result = false;

            if(!string.IsNullOrEmpty(fromCurrencyId) && !string.IsNullOrEmpty(toCurrencyId))
            {
                var mergedList = GetMergedCurrencyList();

                var fromCurrency = _mergedCurrencyList.FirstOrDefault(x => x.Id == fromCurrencyId);
                var toCurrency = _mergedCurrencyList.FirstOrDefault(x => x.Id == toCurrencyId);

                // make sure that the currency being bought is a crypto
                // no sence in having a USD -> EUR purchase
                // or keeping a fiat currency in portfolio
                if(fromCurrency != null && (toCurrency != null && toCurrency.IsCryptoCurrency))
                {
                    // purchase date at least greater than 05-2013
                    if(when > new DateTime(2013, 5, 1))
                    {
                        // price and qty at least bigger than default values
                        if(price > default(decimal) && quantity > default(double))
                        {
                            
                            var purchase = new Purchase
                            {
                                FromCurrency = fromCurrency,
                                ToCurrency = toCurrency as CryptoCurrency,
                                Price = price,
                                Quantity = quantity,
                                TimeStampUTC = when.ToUniversalTime(),
                                Exchange = new Exchange { Name = exchangeId },
                                User = new User { Email = userId }
                            };

                            var fiatCurrencies = mergedList.Where(x => x.IsFiatCurrency)
                                                           .Select(x => x as FiatCurrency).ToList();

                            if(fromCurrency.IsFiatCurrency)
                            {
                                // if the buying currency was already a fiat, then add it to the price estimations list
                                // more frequent scenario is trading from crypto to crypto
                                fiatCurrencies.RemoveAll(x => x.Id == fromCurrency.Id);
                                purchase.PriceEstimations.Add(fromCurrency.Id, price);
                            }

                            var estimatePrices = GetHistoricalPriceEstimations(toCurrency.Id, fiatCurrencies, when);
                            foreach(var estimate in estimatePrices)
                            {
                                var fiat = fiatCurrencies.FirstOrDefault(x => x.Id == estimate.ToCurrencyId);
                                if(fiat != null)
                                {
                                    purchase.PriceEstimations.Add(fiat.Id, estimate.Price);
                                }
                            }

                            var key = _purchaseRepository.Create(purchase);
                            result = key != null;
                        }
                    }
                }
            }
            
            return result;
        }

        public bool DeletePurchase(int id)
        {
            var result = false;

            var purchase = _purchaseRepository.GetSingle(x => x.Id == id);
            if(purchase != null)
            {
                result = _purchaseRepository.Delete(purchase);
            }

            return result;
        }

        public bool UpdatePurchase(Purchase purchase)
        {
            var result = false;

            if (purchase != null)
            {
                result = _purchaseRepository.Update(purchase);
            }

            return result;
        }

        public IEnumerable<Purchase> GetAllPurchases(string userId)
        {
            var result = new List<Purchase>();

            if (!string.IsNullOrEmpty(userId))
            {
                var list = _purchaseRepository.GetPurchasesByUser(userId, true);
                result.AddRange(list);
            }

            return result;
        }

        public Purchase GetSnglePurchase(int id)
        {
            return _purchaseRepository.GetSingle(x => x.Id == id);
        }

        #region [Private]

        private IEnumerable<Currency> GetMergedCurrencyList()
        {
            if (!_mergedCurrencyList.Any())
            {
                lock (_mergedCurrencyListLock)
                {
                    if (!_mergedCurrencyList.Any())
                    {
                        var fiatList = _fiatRepository.GetAll();
                        var cryptoList = _cryptoRepository.GetAll();

                        _mergedCurrencyList.AddRange(fiatList);
                        _mergedCurrencyList.AddRange(cryptoList);
                    }
                }
            }

            return _mergedCurrencyList;
        }

        private IEnumerable<TradePrice> GetHistoricalPriceEstimations(string buyingCurrencyId, IEnumerable<FiatCurrency> fiatCurrencies, DateTime timestamp)
        {
            var result = new List<TradePrice>();

            if(!string.IsNullOrEmpty(buyingCurrencyId) && fiatCurrencies.Any())
            {
                var httpResult = _httpAPI.GetHistoricalPrices(buyingCurrencyId, fiatCurrencies.Select(x => x.Id), timestamp);
                result.AddRange(httpResult);
            }

            return result;
        }

        #endregion
    }
}
