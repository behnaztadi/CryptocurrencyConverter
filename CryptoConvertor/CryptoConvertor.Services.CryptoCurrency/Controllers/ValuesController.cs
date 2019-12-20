using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoConvertor.Services.CryptoCurrency.Application;
using Microsoft.AspNetCore.Mvc;

namespace CryptoConvertor.Services.CryptoCurrency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        ICryptoCurrencyLoaderService _exchangeRateLoaderService;
        public ValuesController(ICryptoCurrencyLoaderService exchangeRateLoaderService)
        {
            _exchangeRateLoaderService = exchangeRateLoaderService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var test = _exchangeRateLoaderService.LoadCryptocurrency("BTC", "USD");

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
