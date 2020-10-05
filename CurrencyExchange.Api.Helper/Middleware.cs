﻿using CurrencyExchange.Helper;
using CurrencyExchange.Helper.Components;
using CurrencyExchange.Helper.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Api.Helper
{
    public class Middleware
    {
        #region [Fields]

        /// <summary>
        /// The RequesyDelegate.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// The Logger.
        /// </summary>
        private readonly ILoggerManager _logger;

        #endregion

        #region [Constructors]

        /// <summary>
        ///  Initializes a new instance of the <see cref="Middleware" /> class.
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public Middleware(RequestDelegate next, ILoggerManager logger)
        {
            _logger = logger;
            _next = next;
        }

        #endregion

        #region [Methods]

        /// <summary>
        /// Custom middleware when request begin and end.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            BeginInvoke(httpContext);
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The Errors Message : ");
                await HandleExceptionAsync(httpContext);
            }
            EndInvoke(httpContext);
        }

        /// <summary>
        /// Custom request begin.
        /// </summary>
        /// <param name="httpContext"></param>
        private void BeginInvoke(HttpContext httpContext)
        {
            _logger.CreateNewSession(httpContext);
            _logger.LogInfo($"About to start {httpContext.Request.Method} {httpContext.Request.GetDisplayUrl()} request");
        }

        /// <summary>
        /// Custom request end.
        /// </summary>
        /// <param name="httpContext"></param>
        private void EndInvoke(HttpContext httpContext)
        {
            _logger.LogInfo($"Request completed with status code: {httpContext.Response.StatusCode} ");
        }

        /// <summary>
        /// Custom when request exception.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        private Task HandleExceptionAsync(HttpContext httpContext, int statusCode = 500)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            var model = UtilityService.InitialResultError(ConstantValue.HttpMessage.Http500Message, statusCode);

            string json = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            return httpContext.Response.WriteAsync(json);
        }

        #endregion

    }
}
