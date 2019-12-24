using CryptoConvertor.Services.CryptoCurrency.Domain.Entities;
using CryptocurrencyConverter.Common.Providers;
using Newtonsoft.Json;
using System.Linq;

namespace CryptoConvertor.Services.CryptoCurrency.Infrastructure.CryptoCurrencyApi.Implementation
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

        public CryptoCurrencyQuote LoadCryptocurrency(string cryptoCurrency, string baseCurrency)
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
