using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoConvertor.Infa.Messaging.Contracts
{
    public interface IExchangeRatesLoaded
    {
        string BaseCurrency { get; }

        string CryptoCurrency { get; }

        Dictionary<string, decimal> LoadedExchangeRates { get; }
    }
}
