using CryptoConvertor.Services.ExchnageRates.Domain.Entities;
using System.Collections.Generic;

namespace CryptoConvertor.Services.ExchnageRates.Application
{
    public interface IExchangeRateApiLoader
    {
        string LoadRatesFromApi(Currency baseCurrency, List<Currency> targetCurrencies);
    }
}
