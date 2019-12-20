using CryptoConvertor.Services.CryptoCurrency.Domain.Entities;
using CryptoConvertor.Services.CryptoCurrency.Infra;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.CryptoCurrency.Application.Implementation.CryptoCurrencyLoaderService
{
    public class CryptoCurrencyLoaderService : ICryptoCurrencyLoaderService
    {
        ICryptocurrencyApiLoader _CryptocurrencyApiLoader;
        ITimeProvider _TimeProvider;

        public CryptoCurrencyLoaderService(ITimeProvider timeProvider, ICryptocurrencyApiLoader cryptocurrencyApiLoader)
        {
            _CryptocurrencyApiLoader = cryptocurrencyApiLoader;
            _TimeProvider = timeProvider;
        }

        public CryptoCurrencyQuote LoadExchangeRates(string cryptoCurrency, string baseCurrency)
        {
            var apiResponce = _CryptocurrencyApiLoader.GetApiResponce(baseCurrency, cryptoCurrency);
            var responce = JsonConvert.DeserializeObject<CryptocurrencyApiResponce>(apiResponce);

            return new CryptoCurrencyQuote(_TimeProvider) 
            {
                BaseCurrency =new Currency(baseCurrency),
                Price = responce.data.First().Value.Quotes.First().Value.Price
            }; 
        }
    }
}
