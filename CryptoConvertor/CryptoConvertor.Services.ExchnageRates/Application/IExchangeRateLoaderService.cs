using CryptoConvertor.Services.ExchnageRates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.ExchnageRates.Application
{
    public interface IExchangeRateLoaderService
    {
        ExchangeRate LoadExchangeRates(Currency currency, List<Currency> targetCurrencies);
    }
}
