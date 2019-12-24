using CryptoConvertor.Infa.Messaging.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.ExchnageRates.Infrastructure.Events
{
    public class ExchangeRatesLoaded : IExchangeRatesLoaded
    {
        public ExchangeRatesLoaded(string baseCurrency, Dictionary<string, decimal> loadedData)
        {
            BaseCurrency = baseCurrency;
            LoadedExchangeRates = loadedData;
        }

        public string BaseCurrency { get; private set; }

        public Dictionary<string, decimal> LoadedExchangeRates { get; private set; }
    }
}
