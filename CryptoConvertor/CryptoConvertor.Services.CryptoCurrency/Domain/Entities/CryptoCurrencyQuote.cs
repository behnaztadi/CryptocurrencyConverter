﻿
using CryptoConvertor.Services.CryptoCurrency.Domain.Exceptions;
using CryptocurrencyConverter.Common.Providers;
using System;

namespace CryptoConvertor.Services.CryptoCurrency.Domain.Entities
{
    public class CryptoCurrencyQuote
    {
        public CryptoCurrencyQuote(ITimeProvider timeProvider)
        {
            Date = timeProvider.CurrentTime;
        }

        public Currency BaseCurrency { get; set; }


        public DateTime Date { get; }

        public decimal _Price;
        public decimal Price
        {
            get { return _Price; }
            set
            {
                if (value <= 0)
                    throw new InvalidPriceException();

                _Price = value;
            }
        }
    }
}
