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
    class CryptoCurrencyQuoteUnitTests
    {
        ITimeProvider _TimeProviderMock = Mock.Of<ITimeProvider>(x => x.CurrentTime == System.DateTime.Parse("2012-01-01"));

        [TestMethod]
        [ExpectedException(typeof(InvalidPriceException))]
        public void Set_Zero_Rate_Would_Fail()
        {
            CryptoCurrencyQuote sut = new CryptoCurrencyQuote(_TimeProviderMock);

            sut.Price = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPriceException))]
        public void Set_Negative_Rate_Would_Fail()
        {
            CryptoCurrencyQuote sut = new CryptoCurrencyQuote(_TimeProviderMock);

            sut.Price = -1;
        }
    }
}
