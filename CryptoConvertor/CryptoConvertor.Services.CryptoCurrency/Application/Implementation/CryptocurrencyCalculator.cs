using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoConvertor.Services.CryptoCurrency.Domain.Entities;
using CryptocurrencyConverter.Common.Providers;

namespace CryptoConvertor.Services.CryptoCurrency.Application.Implementation
{
    public class CryptocurrencyCalculator : ICryptocurrencyCalculator
    {
        ITimeProvider _TimeProvider;
        public CryptocurrencyCalculator(ITimeProvider timeProvider)
        {
            _TimeProvider = timeProvider;
        }

        public List<CryptoCurrencyQuote> Calculate(CryptoCurrencyQuote quoteForBaseCurrency, List<ExchangeRate> exchangeRates)
        {
            if (quoteForBaseCurrency == null || exchangeRates == null || exchangeRates.Count == 0)
                throw new ArgumentException();

            List<CryptoCurrencyQuote> list = new List<CryptoCurrencyQuote>();
            list.Add(new CryptoCurrencyQuote(_TimeProvider) { 
                BaseCurrency = quoteForBaseCurrency.BaseCurrency,
                Price = quoteForBaseCurrency.Price
            });

            foreach (var item in exchangeRates)
            {
                var calculatedPrice = item.Rate * quoteForBaseCurrency.Price;

                list.Add(new CryptoCurrencyQuote(_TimeProvider)
                {
                    Price = calculatedPrice,
                    BaseCurrency = quoteForBaseCurrency.BaseCurrency
                });
            }

            return list;
        }
    }
}
