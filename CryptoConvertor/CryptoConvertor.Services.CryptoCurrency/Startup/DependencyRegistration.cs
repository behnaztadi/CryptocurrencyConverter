using Autofac;
using Autofac.Extensions.DependencyInjection;
using CryptoConvertor.Infa.Messaging.RabbitMq;
using CryptoConvertor.Services.CryptoCurrency.Application;
using CryptoConvertor.Services.CryptoCurrency.Application.Implementation;
using CryptoConvertor.Services.CryptoCurrency.Infrastructure.CryptoCurrencyApi.Implementation;
using CryptoConvertor.Services.CryptoCurrency.Infrastructure.Repositories;
using CryptocurrencyConverter.Common;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace CryptoConvertor.Services.CryptoCurrency
{
    public static class DependencyRegistration
    {
        public static void RegisterAutofac(this ContainerBuilder builder, IConfiguration configuration, IServiceCollection services)
        {
            builder.RegisterModule(new CommonModule());

            builder.RegisterType<CryptocurrencyApiLoader>().As<ICryptocurrencyApiLoader>();
            builder.RegisterType<CryptoCurrencyLoaderService>().As<ICryptoCurrencyLoaderService>();
            builder.RegisterType<CryptocurrencyCalculator>().As<ICryptocurrencyCalculator>(); 
            builder.RegisterType<ConvertConfigRepository>().As<IConvertConfigRepository>(); 

            builder.RegisterConsumers(Assembly.GetExecutingAssembly());
            ConfigureBus(builder, configuration);

            builder.Populate(services);
        }

        // TDOO: Move to messaging and infrastructure 
        private static void ConfigureBus(ContainerBuilder builder, IConfiguration configuration)
        {
            var rabbitMqConfiguration = new RabbitMqConfiguration();
            configuration.GetSection("RabbitMqConnection").Bind(rabbitMqConfiguration);

            builder.Register(context =>
            {
                return Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    var host = config.Host(new Uri(rabbitMqConfiguration.Uri), h => { });

                    config.ReceiveEndpoint(host, rabbitMqConfiguration.ExchangeLoadedQueueName, ep =>
                    {
                        ep.LoadFrom(context);
                    });
                });
            }).As<IBus, IBusControl>().SingleInstance();
        }
    }
}
