using CurrencyExchange.Bll.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {

        #region [Fields]

        private readonly IExchangeBll _exchangeBll;

        #endregion

        #region [Constructors]

        public ExchangeController(IExchangeBll exchangeBll)
        {
            _exchangeBll = exchangeBll;
        }

        #endregion

        #region [Methods]

        [HttpGet]
        [Route("getApple")]
        public IActionResult GetApple(double dollar)
        {
            return Ok(_exchangeBll.GetApple(dollar));
        }

        [HttpGet]
        [Route("getBanana")]
        public IActionResult GetBanana(double dollar)
        {
            return Ok(_exchangeBll.GetBanana(dollar));
        }

        [HttpGet]
        [Route("getDollarWithApple")]
        public IActionResult GetDollarWithApple(double apple)
        {
            return Ok(_exchangeBll.GetDollarWithApple(apple));
        }

        [HttpGet]
        [Route("getDollarWithBanana")]
        public IActionResult GetDollarWithBanana(double banana)
        {
            return Ok(_exchangeBll.GetDollarWithBanana(banana));
        }

        #endregion

    }
}