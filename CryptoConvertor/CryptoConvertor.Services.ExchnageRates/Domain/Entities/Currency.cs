using CryptoConvertor.Services.ExchnageRates.Domain.Exceptions;

namespace CryptoConvertor.Services.ExchnageRates.Domain.Entities
{
    public class Currency
    {
        public Currency(string code)
        {
            Code = code;
        }

        public string Code { get; set; }
    }
}
