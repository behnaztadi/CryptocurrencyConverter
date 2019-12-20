using CryptoConvertor.Services.ExchnageRates.Domain.Entities;
using CryptocurrencyConverter.Common.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace CryptoConvertor.Services.ExchnageRates.Tests
{
    [TestClass]
    public class ExchangeRateUnitTests
    {
        ITimeProvider _TimeProviderMock;

        [TestInitialize]
        public void TestInitilizer()
        {
            _TimeProviderMock = Mock.Of<ITimeProvider>(x => x.CurrentTime == DateTime.Parse("2012-01-01"));
        }

        [TestMethod]
        public void Initilize_ExchangeRate_Set_Date_Using_TimeProvider()
        {
            ExchangeRate sut = new ExchangeRate(_TimeProviderMock);

            Assert.AreEqual(sut.Date, _TimeProviderMock.CurrentTime);
        }
    }
}
