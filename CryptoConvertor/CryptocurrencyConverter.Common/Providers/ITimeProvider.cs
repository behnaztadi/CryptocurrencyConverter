using System;
using System.Collections.Generic;
using System.Text;

namespace CryptocurrencyConverter.Common.Providers
{
    public interface ITimeProvider
    {
        DateTime CurrentTime { get; }
    }
}
