using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyExchange.Helper.Models
{
    public class ResultViewModel
    {
        public bool IsError { get; set; } = false;
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = "Completed";
        public object ModelError { get; set; }
        public object ModelErrorMessage { get; set; }
    }
}
