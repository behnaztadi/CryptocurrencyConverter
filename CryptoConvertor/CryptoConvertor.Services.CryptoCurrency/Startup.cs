using Autofac;
using Autofac.Extensions.DependencyInjection;
using CryptoConvertor.Services.CryptoCurrency.Application;
using CryptoConvertor.Services.CryptoCurrency.Application.Implementation;
using CryptoConvertor.Services.CryptoCurrency.Application.Implementation.CryptoCurrencyLoaderService;
using CryptocurrencyConverter.Common;
using CryptocurrencyConverter.Common.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CryptoConvertor.Services.CryptoCurrency
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc()
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // TODO: extract it as a method 
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new CommonModule());

            containerBuilder.RegisterType<CryptocurrencyApiLoader>().As<ICryptocurrencyApiLoader>();
            containerBuilder.RegisterType<CryptoCurrencyLoaderService>().As<ICryptoCurrencyLoaderService>();

            containerBuilder.Populate(services);

            return new AutofacServiceProvider(containerBuilder.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
