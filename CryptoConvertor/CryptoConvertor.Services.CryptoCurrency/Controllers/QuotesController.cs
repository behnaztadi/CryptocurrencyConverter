using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoConvertor.Infa.Messaging.Contracts;
using CryptoConvertor.Infa.Messaging.RabbitMq;
using CryptoConvertor.Services.CryptoCurrency.Application;
using CryptoConvertor.Services.CryptoCurrency.Infrastructure.CryptoCurrencyApi.Implementation;
using CryptoConvertor.Services.CryptoCurrency.Infrastructure.Messaging;
using CryptoConvertor.Services.CryptoCurrency.Infrastructure.Repositories;
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
        IConvertConfigRepository _ConvertConfigRepository;

        public QuotesController(IBus bus, ICryptoCurrencyLoaderService exchangeRateLoaderService, IConfiguration configuration, IConvertConfigRepository convertConfigRepository)
        {
            _Bus = bus;
            _exchangeRateLoaderService = exchangeRateLoaderService;
            _Configuration = configuration;
            _ConvertConfigRepository = convertConfigRepository;
        }

        [HttpGet]
        public async Task Get(string cryptoCurrency)
        {
            string baseCurrency = _ConvertConfigRepository.GetBaseCurrency();
            string[] currenciesToBeLoaded = _ConvertConfigRepository.GetCrrenciesToConvert();

            var rabbitMqConfiguration = new RabbitMqConfiguration();
            _Configuration.GetSection("RabbitMqConnection").Bind(rabbitMqConfiguration);

            var endpoint = await _Bus.GetSendEndpoint(new Uri(rabbitMqConfiguration.LoadExchangeQueueNameUri));
            await endpoint.Send<ILoadExchangeRates>(new LoadExchangeRatesMessage(cryptoCurrency, baseCurrency, currenciesToBeLoaded));
        }
    }
}
