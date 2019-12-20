using Autofac;
using CryptocurrencyConverter.Common.Providers;

namespace CryptocurrencyConverter.Common
{
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TimeProvider>().As<ITimeProvider>();
        }
    }
}
