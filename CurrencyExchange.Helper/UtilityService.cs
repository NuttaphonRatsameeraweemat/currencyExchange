using CurrencyExchange.Helper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyExchange.Helper
{
    /// <summary>
    /// The Utility Service Class.
    /// </summary>
    public static class UtilityService
    {

        /// <summary>
        /// Initial Error Result and Message to return.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static ResultViewModel InitialResultError(string message, int statusCode = 500, object modelState = null, object modelMessage = null)
        {
            return new ResultViewModel
            {
                IsError = true,
                StatusCode = statusCode,
                Message = message,
                ModelError = modelState,
                ModelErrorMessage = modelMessage
            };
        }
    }

}
