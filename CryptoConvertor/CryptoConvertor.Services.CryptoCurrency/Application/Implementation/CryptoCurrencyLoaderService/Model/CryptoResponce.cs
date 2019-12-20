using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.CryptoCurrency.Application.Implementation
{
    class CryptoResponce
    {
        public string Name { get; set; }
        public string Symbol { get; set; }

        [JsonProperty("Quote")]
        public Dictionary<string, CryptoResponceQuote> Quotes { get; set; }
    }

}
