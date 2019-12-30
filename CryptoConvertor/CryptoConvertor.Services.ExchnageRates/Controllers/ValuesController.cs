using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoConvertor.Infa.Messaging.RabbitMq;
using CryptoConvertor.Services.ExchnageRates.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CryptoConvertor.Services.ExchnageRates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ValuesController()
        {
        }

        //
        // It can be an endpoint to load exchange rates, It could be console as well
        //

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {            
            return new string[] { "value1", "value2" };
        }        
    }
}
