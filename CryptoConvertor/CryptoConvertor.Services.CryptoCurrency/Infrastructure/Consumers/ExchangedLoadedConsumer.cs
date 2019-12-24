using CryptoConvertor.Infa.Messaging.Contracts;
using CryptoConvertor.Services.CryptoCurrency.Application;
using CryptoConvertor.Services.CryptoCurrency.Domain.Entities;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.CryptoCurrency.Infrastructure.Messaging
{
    public class ExchnageLoadedConsumer : IConsumer<IExchangeRatesLoaded>
    {
        IBusControl _Bus;
        ICryptoCurrencyLoaderService _CryptoCurrencyLoaderService;
        ICryptocurrencyCalculator _CryptocurrencyCalculator;
        private IHubContext<NotifyHub, ITypedHubClient> _hubContext;

        public ExchnageLoadedConsumer(IBusControl bus, ICryptoCurrencyLoaderService cryptoCurrencyLoaderService, ICryptocurrencyCalculator cryptocurrencyCalculator, IHubContext<NotifyHub, ITypedHubClient> hubContext)
        {
            _Bus = bus;
            _CryptoCurrencyLoaderService = cryptoCurrencyLoaderService;
            _CryptocurrencyCalculator = cryptocurrencyCalculator;
            _hubContext = hubContext;
        }


        public Task Consume(ConsumeContext<IExchangeRatesLoaded> context)
        {
            Console.WriteLine("It's loaded, {0}", context.Message.LoadedExchangeRates.Count);

            var loadedExchange = context.Message.LoadedExchangeRates.Select(x => new ExchangeRate()
            {
                Currency = new Currency(x.Key),
                Rate = x.Value
            }).ToList();

            var LoadedCurrency = _CryptoCurrencyLoaderService.LoadCryptocurrency("BTC", context.Message.BaseCurrency);

            var testData = _CryptocurrencyCalculator.Calculate(LoadedCurrency, loadedExchange);

            _hubContext.Clients.All.BroadcastMessage("send", "Hahhaa " + testData[0].BaseCurrency.Code);

            return Task.CompletedTask;
        }
    }
}
