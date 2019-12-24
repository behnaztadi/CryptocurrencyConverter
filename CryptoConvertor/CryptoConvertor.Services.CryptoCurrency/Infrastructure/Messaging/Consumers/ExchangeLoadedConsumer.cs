using CryptoConvertor.Infa.Messaging.Contracts;
using CryptoConvertor.Services.CryptoCurrency.Application;
using CryptoConvertor.Services.CryptoCurrency.Domain.Entities;
using CryptoConvertor.Services.CryptoCurrency.Infrastructure.CryptoCurrencyApi.Implementation;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.CryptoCurrency.Infrastructure.Messaging
{
    public class ExchnageLoadedConsumer : IConsumer<IExchangeRatesLoaded>
    {
        ICryptoCurrencyLoaderService _CryptoCurrencyLoaderService;
        ICryptocurrencyCalculator _CryptocurrencyCalculator;
        private IHubContext<NotifyHub, ITypedHubClient> _hubContext;

        public ExchnageLoadedConsumer(ICryptoCurrencyLoaderService cryptoCurrencyLoaderService, ICryptocurrencyCalculator cryptocurrencyCalculator, IHubContext<NotifyHub, ITypedHubClient> hubContext)
        {
            _CryptoCurrencyLoaderService = cryptoCurrencyLoaderService;
            _CryptocurrencyCalculator = cryptocurrencyCalculator;
            _hubContext = hubContext;
        }

        public Task Consume(ConsumeContext<IExchangeRatesLoaded> context)
        {
            var exchangeRates = context.Message.LoadedExchangeRates.Select(x => new ExchangeRate()
            {
                Currency = new Currency(x.Key),
                Rate = x.Value
            }).ToList();

            var loadCryptoCurrencyQuote = _CryptoCurrencyLoaderService.LoadCryptocurrency(context.Message.CryptoCurrency, context.Message.BaseCurrency);

            var finalQuote = _CryptocurrencyCalculator.Calculate(loadCryptoCurrencyQuote, exchangeRates);

            var responceToClient = JsonConvert.SerializeObject(finalQuote);
            _hubContext.Clients.All.BroadcastMessage("send", responceToClient);

            return Task.CompletedTask;
        }
    }
}
