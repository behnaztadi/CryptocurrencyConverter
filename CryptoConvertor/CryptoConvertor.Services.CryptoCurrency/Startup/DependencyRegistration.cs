using Autofac;
using Autofac.Extensions.DependencyInjection;
using CryptoConvertor.Services.CryptoCurrency.Application;
using CryptoConvertor.Services.CryptoCurrency.Application.Implementation;
using CryptoConvertor.Services.CryptoCurrency.Application.Implementation.CryptoCurrencyLoaderService;
using CryptocurrencyConverter.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            builder.Populate(services);
        }
    }
}
