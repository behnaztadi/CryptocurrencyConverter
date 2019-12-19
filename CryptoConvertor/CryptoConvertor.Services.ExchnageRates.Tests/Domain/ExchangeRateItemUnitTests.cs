using CryptoConvertor.Services.ExchnageRates.Domain.Entities;
using CryptoConvertor.Services.ExchnageRates.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoConvertor.Services.ExchnageRates.Tests
{
    [TestClass]
    public class ExchangeRateItemUnitTests 
    {
        [TestMethod]
        [ExpectedException(typeof(NegativeRateException))]
        public void Set_Zero_Rate_Would_Fail()
        {
            ExchangeRateItem sut = new ExchangeRateItem();

            sut.Rate = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(NegativeRateException))]
        public void Set_Negative_Rate_Would_Fail()
        {
            ExchangeRateItem sut = new ExchangeRateItem();

            sut.Rate = -1;
        }
    }
}