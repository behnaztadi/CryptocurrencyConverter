using CryptoConvertor.Infa.Messaging.Contracts;
using CryptoConvertor.Services.ExchnageRates.Application;
using CryptoConvertor.Services.ExchnageRates.Domain.Entities;
using CryptocurrencyConverter.Common.Providers;
using MassTransit;
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

        public ExchnageLoadedConsumer(IBusControl bus, IExchangeRateLoaderService exchangeRateLoaderService, ITimeProvider timeProvider)
        {
            _Bus = bus;
            _ExchangeRateLoaderService = exchangeRateLoaderService;
            _TimeProvider = timeProvider;
        }

        public Task Consume(ConsumeContext<ILoadExchangeRates> context)
        {
            var currenciesToLoad = context.Message.CurrenciesToLoad.Select(x => new Currency(x)).ToList();

            var result = _ExchangeRateLoaderService.LoadExchangeRates(new Domain.Entities.Currency(context.Message.BaseCurrency), currenciesToLoad);

            var messageToSend = new ExchangeRatesLoaded(context.Message.CryptoCurrency, result.BaseCurrency.Code, result.Rates.ToDictionary(x => x.Currency.Code, x => x.Rate));

            var endpoint = _Bus.GetSendEndpoint(new Uri("rabbitmq://localhost/exchange_loaded"));
            endpoint.Result.Send<ExchangeRatesLoaded>(messageToSend);

            return Task.CompletedTask;
        }
    }
}
