using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.CryptoCurrency.Domain.Entities
{
    public class Currency
    {
        public Currency(string code)
        {
            Code = code;
        }

        public string Code { get; set; }
    }
}
