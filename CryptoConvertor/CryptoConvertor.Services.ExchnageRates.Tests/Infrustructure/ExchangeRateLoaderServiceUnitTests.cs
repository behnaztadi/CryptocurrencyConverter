using CryptoConvertor.Services.ExchnageRates.Application;
using CryptoConvertor.Services.ExchnageRates.Application.Implementation;
using CryptoConvertor.Services.ExchnageRates.Domain.Entities;
using CryptoConvertor.Services.ExchnageRates.Infrastructure.ExchangeApi;
using CryptocurrencyConverter.Common.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Text;

namespace CryptoConvertor.Services.ExchnageRates.Tests.Application
{
    [TestClass]
    public class ExchangeRateLoaderServiceUnitTests
    {
        ITimeProvider _TimeProviderMock;
        IExchangeRateApiLoader _ExchangeRateApiLoader;

        ExchangeRate _Expected;

        string _ApiResponce = "{'rates':{'EUR':0.8,'AUD':1.4},'base':'USD','date':'2019-12-19'}";

        [TestInitialize]
        public void TestInitilizer()
        {
            _TimeProviderMock = Mock.Of<ITimeProvider>(x => x.CurrentTime == System.DateTime.Parse("2012-01-01"));
            _ExchangeRateApiLoader = Mock.Of<IExchangeRateApiLoader>(x => x.LoadRatesFromApi(null, null) == _ApiResponce);

            _Expected = new ExchangeRate(_TimeProviderMock)
            {
                BaseCurrency = new Currency("USD"),
                Rates = new List<ExchangeRateItem>()
                {
                    new ExchangeRateItem
                    {
                        Currency= new Currency("EUR"),
                        Rate = 0.8m
                    },
                    new ExchangeRateItem
                    {
                        Currency= new Currency("AUD"),
                        Rate = 1.4m
                    }
                }
            };
        }

        [TestMethod]
        public void Deserilize_JSON_To_Object_Work_As_Expected()
        {
            ExchangeRateLoaderService sut = new ExchangeRateLoaderService(_ExchangeRateApiLoader, _TimeProviderMock);

            var result = sut.LoadExchangeRates(null, null);

            Assert.AreEqual(2, result.Rates.Count);
            Assert.AreEqual(_Expected.Rates[0].Rate, result.Rates[0].Rate);
            Assert.AreEqual(_Expected.Rates[0].Currency.Code, result.Rates[0].Currency.Code);
            Assert.AreEqual(_Expected.Rates[1].Rate, result.Rates[1].Rate);
            Assert.AreEqual(_Expected.Rates[1].Currency.Code, result.Rates[1].Currency.Code);
        }
    }
}