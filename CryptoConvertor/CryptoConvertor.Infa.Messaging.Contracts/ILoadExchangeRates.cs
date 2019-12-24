using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoConvertor.Infa.Messaging.Contracts
{
    public interface ILoadExchangeRates
    {
        string BaseCurrency { get; }
        string[] CurrenciesToLoad { get; }
    }
}
