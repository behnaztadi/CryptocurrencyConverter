using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.ExchnageRates.Core.Exceptions
{
    public class NegativeRateException : Exception
    {
        public NegativeRateException()
            : base($"The rate amount is invalid.")
        {
        }
    }
}
