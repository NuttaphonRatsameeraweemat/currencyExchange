using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyExchange.Data.Interfaces
{
    public interface ICurrencyExchangeData
    {
        /// <summary>
        /// Get base currency exchange apple.
        /// </summary>
        double Apple { get; }
        /// <summary>
        /// Get base currency exchange banana.
        /// </summary>
        double Banana { get; }
    }
}
