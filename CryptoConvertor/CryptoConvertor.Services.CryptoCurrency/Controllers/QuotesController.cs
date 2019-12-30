using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoConvertor.Infa.Messaging.Contracts;
using CryptoConvertor.Infa.Messaging.RabbitMq;
using CryptoConvertor.Services.CryptoCurrency.Application;
using CryptoConvertor.Services.CryptoCurrency.Infrastructure.CryptoCurrencyApi.Implementation;
using CryptoConvertor.Services.CryptoCurrency.Infrastructure.Messaging;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CryptoConvertor.Services.CryptoCurrency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        ICryptoCurrencyLoaderService _exchangeRateLoaderService;
        IBus _Bus;
        IConfiguration _Configuration;

        public QuotesController(IBus bus, ICryptoCurrencyLoaderService exchangeRateLoaderService, IConfiguration configuration)
        {
            _Bus = bus;
            _exchangeRateLoaderService = exchangeRateLoaderService;
            _Configuration = configuration;
        }

        [HttpGet]
        public async Task Get(string cryptoCurrency)
        {
            string baseCurrency = "USD";
            string[] currenciesToBeLoaded = new string[] { "AUD", "EUR", "BRL", "GBP" };

            var rabbitMqConfiguration = new RabbitMqConfiguration();
            _Configuration.GetSection("RabbitMqConnection").Bind(rabbitMqConfiguration);

            var endpoint = await _Bus.GetSendEndpoint(new Uri(rabbitMqConfiguration.ExchangeLoadedQueueNameUri));
            await endpoint.Send<ILoadExchangeRates>(new LoadExchangeRatesMessage(cryptoCurrency, baseCurrency, currenciesToBeLoaded));
        }
    }
}
