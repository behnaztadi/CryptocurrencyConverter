using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.CryptoCurrency
{
    public interface ITypedHubClient
    {
        Task BroadcastMessage(string type, string payload);
    }
}
