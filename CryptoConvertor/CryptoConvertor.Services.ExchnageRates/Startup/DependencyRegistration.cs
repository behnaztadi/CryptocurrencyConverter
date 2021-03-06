﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using CryptoConvertor.Infa.Messaging.Contracts;
using CryptoConvertor.Infa.Messaging.RabbitMq;
using CryptoConvertor.Services.ExchnageRates.Application;
using CryptoConvertor.Services.ExchnageRates.Application.Implementation;
using CryptoConvertor.Services.ExchnageRates.Domain.Entities;
using CryptoConvertor.Services.ExchnageRates.Infrastructure.ExchangeApi;
using CryptocurrencyConverter.Common;
using CryptocurrencyConverter.Common.Providers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.ExchnageRates
{
    public static class DependencyRegistration
    {
        public static void RegisterAutofac(this ContainerBuilder builder, IConfiguration configuration, IServiceCollection services)
        {
            builder.RegisterModule(new CommonModule());

            builder.RegisterType<ExchangeRateApiLoader>().As<IExchangeRateApiLoader>();
            builder.RegisterType<ExchangeRateLoaderService>().As<IExchangeRateLoaderService>();

            builder.RegisterConsumers(Assembly.GetExecutingAssembly());
            ConfigureBus(builder, configuration);

            builder.Populate(services);
        }

        // TDOO: Move to infrastructure 
        private static void ConfigureBus(ContainerBuilder builder, IConfiguration configuration)
        {
            var rabbitMqConfiguration = new RabbitMqConfiguration();
            configuration.GetSection("RabbitMqConnection").Bind(rabbitMqConfiguration);

            builder.Register(context =>
            {
                return Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    var host = config.Host(new Uri(rabbitMqConfiguration.Uri), h => { });

                    config.ReceiveEndpoint(host, rabbitMqConfiguration.LoadExchangeQueueName, ep =>
                    {
                        ep.LoadFrom(context);
                    });
                });

            }).As<IBus, IBusControl>().SingleInstance();
        }
    }
}
