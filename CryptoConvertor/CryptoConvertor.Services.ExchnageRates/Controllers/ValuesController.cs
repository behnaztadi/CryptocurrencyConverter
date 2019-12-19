using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoConvertor.Services.ExchnageRates.Application;
using CryptoConvertor.Services.ExchnageRates.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CryptoConvertor.Services.ExchnageRates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IExchangeRateLoaderService _exchangeRateLoaderService;
        public ValuesController(IExchangeRateLoaderService exchangeRateLoaderService)
        {
            _exchangeRateLoaderService = exchangeRateLoaderService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var test = _exchangeRateLoaderService.LoadExchangeRates(new Currency("USD"),
                   new List<Currency>
                   {
                   new Currency("EUR"),
                   new Currency("AUD")
                   });

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
