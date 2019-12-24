﻿using CryptoConvertor.Infa.Messaging.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.CryptoCurrency.Infrastructure.Messaging
{
    public class LoadExchangeRatesMessage : ILoadExchangeRates
    {
        public LoadExchangeRatesMessage(string baseCurrency, string[] currenciesToLoad)
        {
            BaseCurrency = baseCurrency;
            CurrenciesToLoad = currenciesToLoad;
        }

        public string BaseCurrency { get; private set; }

        public string[] CurrenciesToLoad { get; private set; }
    }
}
