using CryptoConvertor.Services.CryptoCurrency.Application.Implementation;
using CryptoConvertor.Services.CryptoCurrency.Domain.Entities;
using CryptocurrencyConverter.Common.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace CryptoConvertor.Services.CryptoCurrency.Tests.Application
{
    [TestClass]
    public class CryptocurrencyCalculatorUnitTests
    {
        ITimeProvider _TimeProviderMock;

        CryptocurrencyCalculator _sut;
        Currency _BaseCurrency;
        List<ExchangeRate> _ExchnageRates;
        CryptoCurrencyQuote _Quote;


        [TestInitialize]
        public void TestInitilizer()
        {
            _TimeProviderMock = Mock.Of<ITimeProvider>(x => x.CurrentTime == System.DateTime.Parse("2012-01-01"));

            _sut = new CryptocurrencyCalculator(_TimeProviderMock);

            _BaseCurrency = new Currency("USD");

            _ExchnageRates = new List<ExchangeRate>()
            {
                new ExchangeRate { Currency = new Currency("EUR"), Rate = .90m  }
            };

            _Quote = new CryptoCurrencyQuote(_TimeProviderMock)
            {
                BaseCurrency = _BaseCurrency,
                Price = 7200
            };
        }

        [TestMethod]
        public void Calculate_CryptoCurrency_Work_As_Expected_For_A_Single_ExchangeRate()
        {
            var actualResult = _sut.Calculate(_Quote, _ExchnageRates);

            Assert.AreEqual(6480, actualResult[0].Price);

        }

        [TestMethod]
        public void Calculate_CryptoCurrency_Work_As_Expected_For_A_list_Of_ExchangeRates()
        {
            _ExchnageRates.Add(new ExchangeRate { Currency = new Currency("AUD"), Rate = 1.2m });

            var actualResult = _sut.Calculate(_Quote, _ExchnageRates);

            Assert.AreEqual(2, actualResult.Count);

            var euroQuote = actualResult[0];
            Assert.AreEqual(6480, euroQuote.Price);

            var audQuote = actualResult[1];
            Assert.AreEqual(8640, audQuote.Price);
        }


        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void Calculate_CryptoCurrency_Fail_If_Quote_Is_Null()
        {
            var actualResult = _sut.Calculate(null, _ExchnageRates);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void Calculate_CryptoCurrency_Fail_If_ExchangeRates_Is_Null()
        {
            var actualResult = _sut.Calculate(_Quote, null);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void Calculate_CryptoCurrency_Fail_If_ExchangeRates_Is_Empty()
        {
            var actualResult = _sut.Calculate(_Quote, new List<ExchangeRate>());
        }
    }
}
