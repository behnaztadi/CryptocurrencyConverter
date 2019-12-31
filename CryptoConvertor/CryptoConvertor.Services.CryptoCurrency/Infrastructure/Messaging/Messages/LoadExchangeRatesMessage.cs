using CryptoConvertor.Infa.Messaging.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.CryptoCurrency.Infrastructure.Messaging
{
    public class LoadExchangeRatesMessage : ILoadExchangeRates
    {
        public LoadExchangeRatesMessage(string cryptoCurrency, string baseCurrency, string[] currenciesToLoad)
        {
            BaseCurrency = baseCurrency;
            CurrenciesToLoad = currenciesToLoad;
            CryptoCurrency = cryptoCurrency;
        }

        public string BaseCurrency { get; }

        public string CryptoCurrency { get; }

        public string[] CurrenciesToLoad { get; }
    }
}
