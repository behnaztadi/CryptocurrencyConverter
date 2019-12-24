using Autofac;
using Autofac.Extensions.DependencyInjection;
using CryptoConvertor.Services.CryptoCurrency.Application;
using CryptoConvertor.Services.CryptoCurrency.Application.Implementation;
using CryptoConvertor.Services.CryptoCurrency.Application.Implementation.CryptoCurrencyLoaderService;
using CryptocurrencyConverter.Common;
using CryptocurrencyConverter.Common.Providers;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

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
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:51069", "http://localhost:4200")
                .AllowCredentials();
            }));

            services.AddSignalR();

            services.AddMvc()
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterAutofac(Configuration, services);

            var container = containerBuilder.Build();
            container.Resolve<IBusControl>().Start(); // Start listening

            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");
            app.UseSignalR(routes =>
            {
                routes.MapHub<NotifyHub>("/notify");
            });
           
            app.UseMvc();
        }
    }

    public class NotifyHub : Hub<ITypedHubClient>
    {
    }

    public interface ITypedHubClient
    {
        Task BroadcastMessage(string type, string payload);
    }
}
