using CryptoConvertor.Services.CryptoCurrency.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.CryptoCurrency.Application
{
    public interface ICryptocurrencyCalculator
    {
        List<CryptoCurrencyQuote> Calculate(CryptoCurrencyQuote quoteForBaseCurrency ,List<ExchangeRate> exchangeRates); 
    }
}
