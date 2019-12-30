using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.CryptoCurrency.Infrastructure.Repositories
{
    public class ConvertConfigRepository : IConvertConfigRepository
    {
        public string GetBaseCurrency()
        {
            return "USD";
        }

        public string[] GetCrrenciesToConvert()
        {
            return new string[] { "AUD", "EUR", "BRL", "GBP" };
        }
    }
}
