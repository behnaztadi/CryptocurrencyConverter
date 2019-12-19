using CryptoConvertor.Services.ExchnageRates.Domain.Exceptions;

namespace CryptoConvertor.Services.ExchnageRates.Domain.Entities
{
    public class ExchangeRateItem
    {
        public Currency Currency { get; set; }

        public decimal _Rate;
        public decimal Rate
        {
            get { return _Rate; }
            set
            {
                if (value <= 0)
                    throw new NegativeRateException();

                _Rate = value;
            }
        }
    }
}
