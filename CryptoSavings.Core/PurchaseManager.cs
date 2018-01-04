using System;
using CryptoSavings.Contracts.Core;
using CryptoSavings.Model;
using System.Collections.Generic;
using CryptoSavings.Model.DAL.HttpAPI;
using CryptoSavings.Contracts.Repository;
using System.Linq;

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

        #region [CTOR]

        public PurchaseManager(IFiatRepository fiatRepository, ICryptoRepository cryptoRepository, IPurchaseRepository purchaseRepository)
        {
            _fiatRepository = fiatRepository ?? throw new ArgumentNullException(nameof(fiatRepository));
            _cryptoRepository = cryptoRepository ?? throw new ArgumentNullException(nameof(cryptoRepository));
            _purchaseRepository = purchaseRepository ?? throw new ArgumentNullException(nameof(purchaseRepository));
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

                if(fromCurrency != null && toCurrency != null)
                {
                    // purchase date at least not more than 10 years ago
                    if(when > new DateTime(2007, 1, 1))
                    {
                        // price and qty at least bigger than default values
                        if(price > default(decimal) && quantity > default(double))
                        {
                            var tradePrice = new TradePrice
                            {
                                FromCurrency = fromCurrency,
                                ToCurrency = toCurrency,
                                Price = price,
                                TimeStampUTC = when.ToUniversalTime()
                            };

                            var purchase = new Purchase
                            {
                                Quantity = quantity,
                                TradePrice = tradePrice,
                                Exchange = new Exchange { Name = exchangeId },
                                User = new User { Email = userId }
                            };

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

        #endregion
    }
}
