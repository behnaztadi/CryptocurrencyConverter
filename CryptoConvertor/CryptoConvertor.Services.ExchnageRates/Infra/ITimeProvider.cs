using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.ExchnageRates.Infra
{
    public interface ITimeProvider
    {
        DateTime CurrentTime { get; }
    }
}
