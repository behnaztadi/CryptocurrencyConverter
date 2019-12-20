using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.CryptoCurrency.Infra
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime CurrentTime
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
