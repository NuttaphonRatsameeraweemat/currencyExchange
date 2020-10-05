using CurrencyExchange.Bll.Interfaces;
using CurrencyExchange.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyExchange.Bll
{
    public class ExchangeBll : IExchangeBll
    {

        #region [Fields]

        private readonly ICurrencyExchangeData _currencyExchangeData;

        #endregion

        #region [Constructors]

        public ExchangeBll(ICurrencyExchangeData currencyExchangeData)
        {
            _currencyExchangeData = currencyExchangeData;
        }

        #endregion

        #region [Methods]

        private double ExchangeToTarget(double dollar, double baseTarget) => dollar * baseTarget;

        private double ExchangeToDollar(double amount, double baseTarget) => amount % baseTarget;

        public double GetApple(double dollar) => dollar > 0 ? ExchangeToTarget(dollar, _currencyExchangeData.Apple) : 0;

        public double GetBanana(double dollar) => dollar > 0 ? ExchangeToTarget(dollar, _currencyExchangeData.Banana) : 0;

        public double GetDollarWithApple(double apple) => apple > 0 ? ExchangeToDollar(apple, _currencyExchangeData.Apple) : 0;

        public double GetDollarWithBanana(double banana) => banana > 0 ? ExchangeToDollar(banana, _currencyExchangeData.Banana) : 0;

        #endregion

    }
}
