using CryptoSavings.Contracts.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CryptoSavings.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly IPortfolioManager _portfolioManager;

        public ValuesController(ILogger<ValuesController> logger, IPortfolioManager portfolioManager)

        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _portfolioManager = portfolioManager ?? throw new ArgumentNullException(nameof(portfolioManager));
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public int Get(int id)
        {
            _logger.LogDebug("Get method called with id:{0}", id);
            return id;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            _portfolioManager.GetUserPortfoilo(new Model.User());
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
