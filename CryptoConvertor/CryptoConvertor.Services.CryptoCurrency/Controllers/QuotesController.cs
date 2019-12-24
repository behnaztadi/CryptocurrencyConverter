using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoConvertor.Infa.Messaging.Contracts;
using CryptoConvertor.Services.CryptoCurrency.Application;
using CryptoConvertor.Services.CryptoCurrency.Infrastructure.CryptoCurrencyApi.Implementation;
using CryptoConvertor.Services.CryptoCurrency.Infrastructure.Messaging;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace CryptoConvertor.Services.CryptoCurrency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        ICryptoCurrencyLoaderService _exchangeRateLoaderService;
        IBus _Bus;
        public QuotesController(IBus bus, ICryptoCurrencyLoaderService exchangeRateLoaderService)
        {
            _Bus = bus;
            _exchangeRateLoaderService = exchangeRateLoaderService;
        }

        [HttpGet]
        public async Task Get(string cryptoCurrency)
        {
            // it could come from UI side
            string baseCurrency = "USD";
            string[] currenciesToBeLoaded = new string[] { "AUD", "EUR" };

            var endpoint = await _Bus.GetSendEndpoint(new Uri("rabbitmq://localhost/load_exchange-queue"));
            await endpoint.Send<ILoadExchangeRates>(new LoadExchangeRatesMessage(cryptoCurrency, baseCurrency, currenciesToBeLoaded));
        }
    }
}
