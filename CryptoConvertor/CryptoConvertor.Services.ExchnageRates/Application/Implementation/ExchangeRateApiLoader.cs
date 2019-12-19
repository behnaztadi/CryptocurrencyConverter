using CryptoConvertor.Services.ExchnageRates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace CryptoConvertor.Services.ExchnageRates.Application.Implementation
{
    public class ExchangeRateApiLoader : IExchangeRateApiLoader
    {
        public string LoadRatesFromApi(Currency baseCurrency, List<Currency> targetCurrencies)
        {
            var uriBuilder = new UriBuilder("https://api.exchangeratesapi.io/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["symbols"] = string.Join(",", targetCurrencies.Select(x => x.Code).ToArray());
            queryString["base"] = baseCurrency.Code;

            uriBuilder.Query = queryString.ToString();

            var client = new WebClient();
            return client.DownloadString(uriBuilder.ToString());
        }
    }
}
