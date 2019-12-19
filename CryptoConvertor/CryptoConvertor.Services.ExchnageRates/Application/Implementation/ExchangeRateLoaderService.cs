using CryptoConvertor.Services.ExchnageRates.Domain.Entities;
using CryptoConvertor.Services.ExchnageRates.Infra;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CryptoConvertor.Services.ExchnageRates.Application.Implementation
{
    public class ExchangeRateLoaderService : IExchangeRateLoaderService
    {
        IExchangeRateApiLoader _ExchangeRateApiLoader;
        ITimeProvider _TimeProvider;

        public ExchangeRateLoaderService(IExchangeRateApiLoader exchangeRateApiLoader, ITimeProvider timeProvider)
        {
            _ExchangeRateApiLoader = exchangeRateApiLoader;
            _TimeProvider = timeProvider;
        }

        public ExchangeRate LoadExchangeRates(Currency baseCurrency, List<Currency> targetCurrencies)
        {
            var responce = _ExchangeRateApiLoader.LoadRatesFromApi(baseCurrency, targetCurrencies);
            var rates = JsonConvert.DeserializeAnonymousType(responce, new { rates = new Dictionary<string, decimal>() }).rates;

            return new ExchangeRate(_TimeProvider)
            {
                BaseCurrency = baseCurrency,
                Rates = rates.Select(x => new ExchangeRateItem
                {
                    Currency = new Currency(x.Key),
                    Rate = x.Value
                }).ToList()
            };
        }
    }
}
