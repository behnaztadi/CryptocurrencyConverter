using CryptoConvertor.Services.CryptoCurrency.Domain.Entities;
using CryptoConvertor.Services.CryptoCurrency.Domain.Exceptions;
using CryptocurrencyConverter.Common.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoConvertor.Services.CryptoCurrency.Tests.Domain
{
    [TestClass]
    class ExchangeRateUnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidPriceException))]
        public void Set_Zero_Rate_Would_Fail()
        {
            ExchangeRate sut = new ExchangeRate();

            sut.Rate = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPriceException))]
        public void Set_Negative_Rate_Would_Fail()
        {
            ExchangeRate sut = new ExchangeRate();

            sut.Rate = -1;
        }
    }
}
