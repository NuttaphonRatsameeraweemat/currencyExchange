using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyExchange.Bll.Interfaces
{
    public interface IExchangeBll
    {
        double GetApple(double dollar);
        double GetBanana(double dollar);
        double GetDollarWithApple(double apple);
        double GetDollarWithBanana(double banana);
    }
}
