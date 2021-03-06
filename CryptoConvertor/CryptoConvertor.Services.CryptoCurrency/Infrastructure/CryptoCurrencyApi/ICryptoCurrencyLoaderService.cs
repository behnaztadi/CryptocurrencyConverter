﻿using CryptoConvertor.Services.CryptoCurrency.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.CryptoCurrency.Infrastructure.CryptoCurrencyApi.Implementation
{
    public interface ICryptoCurrencyLoaderService
    {
        CryptoCurrencyQuote LoadCryptocurrency(string cryptoCurrency, string baseCurrency);
    }
}
