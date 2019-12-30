using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace CryptoConvertor.Services.CryptoCurrency.Infrastructure.CryptoCurrencyApi.Implementation
{
    public class CryptocurrencyApiLoader : ICryptocurrencyApiLoader
    {
        public CryptocurrencyApiLoader(IConfiguration configuration)
        {
            _ApiKey = configuration.GetValue<string>("CryptoConvertorApiKey");
        }

        string _ApiKey;

        public string GetApiResponce(string baseCurrency, string cryptoCurrensy)
        {
            var urlBuilder = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["convert"] = baseCurrency;
            queryString["symbol"] = cryptoCurrensy;

            urlBuilder.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", _ApiKey);
            client.Headers.Add("Accepts", "application/json");
            return client.DownloadString(urlBuilder.ToString());
        }
    }
}
