using CryptoConvertor.Services.ExchnageRates.Core.Entities;
using CryptoConvertor.Services.ExchnageRates.Infra;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace CryptoConvertor.Services.ExchnageRates.Application.Implementation
{
    public class ExchangeRateLoaderService : IExchangeRateLoaderService
    {
        IConfiguration _Configuration;
        ITimeProvider _TimeProvider;
        public ExchangeRateLoaderService(IConfiguration configuration, ITimeProvider timeProvider)
        {
            _Configuration = configuration;
            _TimeProvider = timeProvider;
        }

        public ExchangeRate LoadExchangeRates(Currency baseCurrency, List<Currency> targetCurrencies)
        {
            var uriBuilder = new UriBuilder("https://api.exchangeratesapi.io/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["symbols"] = string.Join(",", targetCurrencies.Select(x => x.Code).ToArray());
            queryString["base"] = baseCurrency.Code;

            uriBuilder.Query = queryString.ToString();

            var client = new WebClient();
            var responce = client.DownloadString(uriBuilder.ToString());

            var rates = JsonConvert.DeserializeAnonymousType(responce, new { rates = new Dictionary<string, decimal>() }).rates;

            return new ExchangeRate(_TimeProvider)
            {
                BaseCurrency = baseCurrency,
                Rates = rates.Select(x => new ExchangeRateItem
                {
                    Currency = new Currency(x.Key),
                    Rate = x.Value
                }).ToList()
            };
        }
    }
}
