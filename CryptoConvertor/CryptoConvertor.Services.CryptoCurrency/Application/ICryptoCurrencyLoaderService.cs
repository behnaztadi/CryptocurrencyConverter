using CryptoConvertor.Services.CryptoCurrency.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.CryptoCurrency.Application
{
    public interface ICryptoCurrencyLoaderService
    {
        CryptoCurrencyQuote LoadExchangeRates(string cryptoCurrency, string baseCurrency);
    }
}
