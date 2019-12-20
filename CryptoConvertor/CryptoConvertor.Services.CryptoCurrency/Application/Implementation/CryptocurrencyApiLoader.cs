using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace CryptoConvertor.Services.CryptoCurrency.Application.Implementation
{
    public class CryptocurrencyApiLoader : ICryptocurrencyApiLoader
    {
        string _ApiKey = "16187df9-bb5a-4f41-865b-d90cd26f1701";

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
