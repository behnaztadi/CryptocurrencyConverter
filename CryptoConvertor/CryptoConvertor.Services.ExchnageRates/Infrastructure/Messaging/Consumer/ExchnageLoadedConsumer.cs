using CryptoConvertor.Infa.Messaging.Contracts;
using CryptoConvertor.Infa.Messaging.RabbitMq;
using CryptoConvertor.Services.ExchnageRates.Application;
using CryptoConvertor.Services.ExchnageRates.Domain.Entities;
using CryptocurrencyConverter.Common.Providers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.ExchnageRates.Infrastructure.Messaging
{
    public class ExchnageLoadedConsumer : IConsumer<ILoadExchangeRates>
    {
        IBusControl _Bus;
        IExchangeRateLoaderService _ExchangeRateLoaderService;
        ITimeProvider _TimeProvider;
        IConfiguration _Configuration;

        public ExchnageLoadedConsumer(IBusControl bus, IExchangeRateLoaderService exchangeRateLoaderService, ITimeProvider timeProvider, IConfiguration configuration)
        {
            _Bus = bus;
            _ExchangeRateLoaderService = exchangeRateLoaderService;
            _TimeProvider = timeProvider;
            _Configuration = configuration;
        }

        public Task Consume(ConsumeContext<ILoadExchangeRates> context)
        {
            var currenciesToLoad = context.Message.CurrenciesToLoad.Select(x => new Currency(x)).ToList();

            var result = _ExchangeRateLoaderService.LoadExchangeRates(new Domain.Entities.Currency(context.Message.BaseCurrency), currenciesToLoad);

            var messageToSend = new ExchangeRatesLoaded(context.Message.CryptoCurrency, result.BaseCurrency.Code, result.Rates.ToDictionary(x => x.Currency.Code, x => x.Rate));

            var rabbitMqConfiguration = new RabbitMqConfiguration();
            _Configuration.GetSection("RabbitMqConnection").Bind(rabbitMqConfiguration);

            var endpoint = _Bus.GetSendEndpoint(new Uri(rabbitMqConfiguration.ExchangeLoadedQueueNameUri));
            endpoint.Result.Send<ExchangeRatesLoaded>(messageToSend);

            return Task.CompletedTask;
        }
    }
}
