using CurrencyExchange.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyExchange.Data
{
    public class CurrencyExchangeData : ICurrencyExchangeData
    {

        #region [Fields]

        /// <summary>
        /// The config value in appsetting.json
        /// </summary>
        private readonly IConfiguration _config;

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyExchangeData" /> class.
        /// </summary>
        /// <param name="config">The config value.</param>
        public CurrencyExchangeData(IConfiguration config)
        {
            _config = config;
        }

        #endregion

        #region [Methods]

        /// <summary>
        /// Read value in appsetting config with key name;
        /// </summary>
        /// <param name="name">The key name parameter in appsetting.</param>
        /// <returns></returns>
        private string GetAppSetting(string name) => _config[name];

        /// <summary>
        /// Get base currency exchange apple.
        /// </summary>
        public double Apple => double.TryParse(this.GetAppSetting("Apple"), out double apple) ? apple : 0;
        /// <summary>
        /// Get base currency exchange banana.
        /// </summary>
        public double Banana => double.TryParse(this.GetAppSetting("Banana"), out double banana) ? banana : 0;

        #endregion

    }
}
