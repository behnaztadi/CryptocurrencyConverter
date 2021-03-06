﻿using CryptoConvertor.Infa.Messaging.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.ExchnageRates.Infrastructure.Messaging
{
    public class ExchangeRatesLoaded : IExchangeRatesLoaded
    {
        public ExchangeRatesLoaded(string cryptoCurrency,string baseCurrency, Dictionary<string, decimal> loadedData)
        {
            BaseCurrency = baseCurrency;
            LoadedExchangeRates = loadedData;
            CryptoCurrency = cryptoCurrency;
        }

        public string BaseCurrency { get; }

        public Dictionary<string, decimal> LoadedExchangeRates { get; }

        public string CryptoCurrency { get; }
    }
}
