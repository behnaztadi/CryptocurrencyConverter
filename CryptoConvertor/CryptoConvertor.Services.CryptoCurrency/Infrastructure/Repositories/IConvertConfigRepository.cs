using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.CryptoCurrency.Infrastructure.Repositories
{
    public interface IConvertConfigRepository
    {
        string GetBaseCurrency();
        string[] GetCrrenciesToConvert();
    }
}
