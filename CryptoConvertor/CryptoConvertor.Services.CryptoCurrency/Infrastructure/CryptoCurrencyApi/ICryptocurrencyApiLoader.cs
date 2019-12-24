using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.CryptoCurrency.Infrastructure.CryptoCurrencyApi.Implementation
{
    public interface ICryptocurrencyApiLoader
    {
        string GetApiResponce(string baseCurrency, string cryptoCurrensy);
    }
}
