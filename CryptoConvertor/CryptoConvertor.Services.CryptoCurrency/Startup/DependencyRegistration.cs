using Autofac;
using Autofac.Extensions.DependencyInjection;
using CryptoConvertor.Infa.Messaging.Contracts;
using CryptoConvertor.Infa.Messaging.RabbitMq;
using CryptoConvertor.Services.CryptoCurrency.Application;
using CryptoConvertor.Services.CryptoCurrency.Application.Implementation;
using CryptoConvertor.Services.CryptoCurrency.Application.Implementation.CryptoCurrencyLoaderService;
using CryptoConvertor.Services.CryptoCurrency.Domain.Entities;
using CryptocurrencyConverter.Common;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static MassTransit.Logging.OperationName;

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

            builder.RegisterConsumers(Assembly.GetExecutingAssembly());
            ConfigureBus(builder,configuration);

            builder.Populate(services);
        }

        // TDOO: Move to messaging and infrastructure 
        private static void ConfigureBus(ContainerBuilder builder, IConfiguration configuration)
        {
            var connection= new RabbitMqConnection();
            configuration.GetSection("RabbitMqConnection").Bind(connection);

            builder.Register(context =>
            {
                return Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    var host = config.Host(new Uri(connection.HostName), h =>
                    {
                        h.Username(connection.UserName);
                        h.Password(connection.Password);
                    });

                    config.ReceiveEndpoint(host, "exchange_loaded", ep =>
                    {
                        ep.LoadFrom(context);
                    });
                });
            }).As<IBus, IBusControl>().SingleInstance();
        }
    }
}
