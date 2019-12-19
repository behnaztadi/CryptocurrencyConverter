using CryptoConvertor.Services.ExchnageRates.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.ExchnageRates.Core.Entities
{
    public class ExchangeRate
    {
        public ExchangeRate(ITimeProvider timeProvider)
        {
            Rates = new List<ExchangeRateItem>();
            LoadedDate = timeProvider.CurrentTime;
        }

        public Currency BaseCurrency { get; set; }

        public List<ExchangeRateItem> Rates { get; set; }

        public DateTime LoadedDate { get; private set; }
    }
}
