using Autofac;
using Autofac.Extensions.DependencyInjection;
using CryptoConvertor.Services.ExchnageRates.Application;
using CryptoConvertor.Services.ExchnageRates.Application.Implementation;
using CryptocurrencyConverter.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.ExchnageRates.Startup
{
    public static class DependencyRegistration
    {
        public static void RegisterAutofac(this ContainerBuilder builder, IConfiguration configuration, IServiceCollection services)
        {
            builder.RegisterModule(new CommonModule());

            builder.RegisterType<ExchangeRateApiLoader>().As<IExchangeRateApiLoader>();
            builder.RegisterType<ExchangeRateLoaderService>().As<IExchangeRateLoaderService>();
            
            builder.Populate(services);
        }
    }
}
