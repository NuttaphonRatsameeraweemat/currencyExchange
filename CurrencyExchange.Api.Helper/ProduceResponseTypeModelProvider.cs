using CurrencyExchange.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyExchange.Api.Helper
{
    public class ProduceResponseTypeModelProvider : IApplicationModelProvider
    {
        public int Order => 3;

        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        {
        }

        /// <summary>
        /// Add produces response type to attribute filter.
        /// </summary>
        /// <param name="context">The context object for <see cref="IApplicationModelProvider" /></param>
        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        {
            foreach (ControllerModel controller in context.Result.Controllers)
            {
                foreach (ActionModel action in controller.Actions)
                {
                    var genericReturnType = action.ActionMethod.ReturnType.GenericTypeArguments;
                    if (genericReturnType.Length > 0)
                    {
                        action.Filters.Add(new ProducesResponseTypeAttribute(genericReturnType[0],
                                                                             StatusCodes.Status200OK));
                    }

                    action.Filters.Add(new ProducesResponseTypeAttribute(UtilityService.InitialResultError("").GetType(),
                                                                         StatusCodes.Status400BadRequest));
                    action.Filters.Add(new ProducesResponseTypeAttribute(UtilityService.InitialResultError("").GetType(),
                                                                         StatusCodes.Status401Unauthorized));
                    action.Filters.Add(new ProducesResponseTypeAttribute(UtilityService.InitialResultError("").GetType(),
                                                                         StatusCodes.Status403Forbidden));
                    action.Filters.Add(new ProducesResponseTypeAttribute(UtilityService.InitialResultError("").GetType(),
                                                                         StatusCodes.Status500InternalServerError));
                }
            }
        }
    }
}
