using CryptocurrencyConverter.Common.Providers;
using System;
using System.Collections.Generic;

namespace CryptoConvertor.Services.ExchnageRates.Domain.Entities
{
    public class ExchangeRate
    {
        public ExchangeRate(ITimeProvider timeProvider)
        {
            Rates = new List<ExchangeRateItem>();
            Date = timeProvider.CurrentTime;
        }

        public Currency BaseCurrency { get; set; }

        public List<ExchangeRateItem> Rates { get; set; }

        public DateTime Date { get; private set; }
    }
}
