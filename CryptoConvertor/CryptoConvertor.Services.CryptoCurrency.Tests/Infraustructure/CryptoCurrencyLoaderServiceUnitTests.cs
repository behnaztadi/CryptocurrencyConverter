using CryptoConvertor.Services.CryptoCurrency.Domain.Entities;
using CryptoConvertor.Services.CryptoCurrency.Infrastructure.CryptoCurrencyApi.Implementation;
using CryptocurrencyConverter.Common.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CryptoConvertor.Services.CryptoCurrency.Tests.Application
{
    [TestClass]
    public class CryptoCurrencyLoaderServiceUnitTests
    {

        ITimeProvider _TimeProviderMock;
        ICryptocurrencyApiLoader _CryptocurrencyApiLoader;
        CryptoCurrencyQuote _Expected;

        string _ApiResponce = @"{
                                'status': {
                                    'timestamp': '2019-12-20T18:16:22.428Z',
                                    'error_code': 0,
                                    'error_message': null,
                                    'elapsed': 9,
                                    'credit_count': 1,
                                    'notice': null
                                },
                                'data': {
                                    'BTC': {
                                        'id': 1,
                                        'name': 'Bitcoin',
                                        'symbol': 'BTC',
                                        'slug': 'bitcoin',
                                        'num_market_pairs': 7595,
                                        'date_added': '2013-04-28T00:00:00.000Z',
                                        'tags': [
                                            'mineable'
                                        ],
                                        'max_supply': 21000000,
                                        'circulating_supply': 18112237,
                                        'total_supply': 18112237,
                                        'platform': null,
                                        'cmc_rank': 1,
                                        'last_updated': '2019-12-20T18:15:33.000Z',
                                        'quote': {
                                            'USD': {
                                                'price': 7219.71944582,
                                                'volume_24h': 22262712655.2271,
                                                'percent_change_1h': 0.271659,
                                                'percent_change_24h': 0.995734,
                                                'percent_change_7d': -0.64111,
                                                'market_cap': 130765269676.2005,
                                                'last_updated': '2019-12-20T18:15:33.000Z'
                                            }
                                        }
                                    }
                                }
                            }";


        [TestMethod]
        public void Deserilize_JSON_To_Object_Work_As_Expected()
        {
            _TimeProviderMock = Mock.Of<ITimeProvider>(x => x.CurrentTime == System.DateTime.Parse("2012-01-01"));
            _CryptocurrencyApiLoader = Mock.Of<ICryptocurrencyApiLoader>(x => x.GetApiResponce(null, null) == _ApiResponce);

            // Action
            CryptoCurrencyLoaderService sut = new CryptoCurrencyLoaderService(_TimeProviderMock, _CryptocurrencyApiLoader);
            var actualResult = sut.LoadCryptocurrency(null, null);

            // Assertation
            Assert.AreEqual(7219.71944582m, actualResult.Price);
        }
    }
}
