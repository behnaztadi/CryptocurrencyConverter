using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.CryptoCurrency.Domain.Exceptions
{
    public class InvalidPriceException : Exception
    {
        public InvalidPriceException()
            : base($"The price amount is invalid.")
        {
        }
    }
}
