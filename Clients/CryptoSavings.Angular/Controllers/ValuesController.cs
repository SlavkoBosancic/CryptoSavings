using CryptoSavings.Angular.Helpers.Models;
using CryptoSavings.Contracts.Core;
using CryptoSavings.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CryptoSavings.Angular.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly IApplicationManager _applicationManager;
        private readonly IPurchaseManager _purchaseManager;

        public ValuesController(ILogger<ValuesController> logger, IApplicationManager applicationManager, IPurchaseManager purchaseManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _applicationManager = applicationManager ?? throw new ArgumentNullException(nameof(applicationManager));
            _purchaseManager = purchaseManager ?? throw new ArgumentNullException(nameof(purchaseManager));
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Purchase> Get()
        {
            var demoUser = _applicationManager.GetDemoUser();
            return _purchaseManager.GetAllPurchases(demoUser.Email);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Purchase Get(int id)
        {
            return _purchaseManager.GetSnglePurchase(id);
        }

        // POST api/values
        [HttpPost]
        public bool Post([FromBody]PurchaseContainer purchaseContainer)
        {
            var result = false;

            if (purchaseContainer != null)
            {
                var demoUser = _applicationManager.GetDemoUser();
                result = _purchaseManager.CreatePurchase(fromCurrencyId: purchaseContainer.FromCurrencyId,
                                                         toCurrencyId: purchaseContainer.ToCurrencyId,
                                                         when: purchaseContainer.When,
                                                         price: purchaseContainer.Price,
                                                         quantity: purchaseContainer.Quantity,
                                                         exchangeId: purchaseContainer.ExchangeId,
                                                         userId: demoUser.Email);
            }

            return result;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody]Purchase purchase)
        {
            return _purchaseManager.UpdatePurchase(purchase);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _purchaseManager.DeletePurchase(id);
        }
    }
}
