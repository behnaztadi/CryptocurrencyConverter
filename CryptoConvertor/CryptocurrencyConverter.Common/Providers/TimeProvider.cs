using System;
using System.Collections.Generic;
using System.Text;

namespace CryptocurrencyConverter.Common.Providers
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime CurrentTime
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
