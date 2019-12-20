using CryptoConvertor.Services.CryptoCurrency.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoConvertor.Services.CryptoCurrency.Domain.Entities
{
    public class ExchangeRate
    {
        public Currency Currency { get; set; }

        public decimal _Rate;
        public decimal Rate
        {
            get { return _Rate; }
            set
            {
                if (value <= 0)
                    throw new InvalidPriceException();

                _Rate = value;
            }
        }
    }
}
