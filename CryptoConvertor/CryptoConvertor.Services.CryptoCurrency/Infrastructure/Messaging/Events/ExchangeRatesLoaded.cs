using CryptoConvertor.Infa.Messaging.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.CryptoCurrency.Infrastructure.Messaging
{
    public class ExchangeRatesLoaded : IExchangeRatesLoaded
    {
        public ExchangeRatesLoaded(string cryptoCurrency, string baseCurrency, Dictionary<string, decimal> exchangeRates)
        {
            BaseCurrency = baseCurrency;
            LoadedExchangeRates = exchangeRates;
            CryptoCurrency = cryptoCurrency;
        }

        public string BaseCurrency { get; }

        public Dictionary<string, decimal> LoadedExchangeRates { get; }

        public string CryptoCurrency { get; private set; }
    }
}
