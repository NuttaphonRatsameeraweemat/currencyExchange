using CurrencyExchange.Api.Helper;
using CurrencyExchange.Bll;
using CurrencyExchange.Bll.Interfaces;
using CurrencyExchange.Data;
using CurrencyExchange.Data.Interfaces;
using CurrencyExchange.Helper;
using CurrencyExchange.Helper.Components;
using CurrencyExchange.Helper.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.Api.Extentions
{
    public static class ServiceExtensions
    {

        /// <summary>
        /// Dependency Injection data layer class.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void ConfigureDataLayer(this IServiceCollection services)
        {
            services.AddTransient<ICurrencyExchangeData, CurrencyExchangeData>();
        }

        /// <summary>
        /// Dependency Injection business later class..
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void ConfigureBusniessLayer(this IServiceCollection services)
        {
            services.AddScoped<IExchangeBll, ExchangeBll>();
        }

        /// <summary>
        /// Register service components class.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void ConfigureComponent(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        /// <summary>
        /// Configure adding mvc and configure prefix route and filter.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureMvc(this IServiceCollection services)
        {
            services.AddMvc(opt =>
            {
                opt.UseApiGlobalConfigRoutePrefix(new RouteAttribute("api"));
                opt.Filters.Add(typeof(ValidateModelStateAttribute));
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    return new BadRequestObjectResult(
                        UtilityService.InitialResultError(ConstantValue.HttpMessage.Http400Message, (int)System.Net.HttpStatusCode.BadRequest,
                                        actionContext.ModelState.Keys,
                                        actionContext.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
                };
            });
        }

        /// <summary>
        /// Config Api Routes Prefix.
        /// </summary>
        /// <param name="opts">The MvcOptions.</param>
        /// <param name="routeAttribute">The IRouteTemplateProvider.</param>
        public static void UseApiGlobalConfigRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
        {
            opts.Conventions.Insert(0, new ApiGlobalPrefixRouteProvider(routeAttribute));
        }

        /// <summary>
        /// Dependency Injection produce response type attribute filter.
        /// </summary>
        /// <param name="services">>The service collection.</param>
        public static void ConfigureProduceResponseType(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider,
                                                        ProduceResponseTypeModelProvider>());
        }

        /// <summary>
        /// Add CORS Configuration.
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        /// <summary>
        /// Configuration Swaager Doc and Authentication type.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SendDi API", Version = "v1" });
                c.GenOpenApiSecurityScheme("Bearer", "Please insert JWT with Bearer into field", "Authorization");
                c.GenOpenApiSecurityRequirement("Bearer");
            });
        }

        /// <summary>
        /// Add one or more security definition to the generated swagger.
        /// </summary>
        /// <param name="swaggerGen">The swagger gen options.</param>
        /// <param name="name">The name of api security scheme.</param>
        /// <param name="description">The description in api security scheme.</param>
        /// <param name="key">The key name in api security scheme.</param>
        private static void GenOpenApiSecurityScheme(this SwaggerGenOptions swaggerGen,
                                                     string name, string description, string key)
        {
            swaggerGen.AddSecurityDefinition(name, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = description,
                Name = key,
                Type = SecuritySchemeType.ApiKey
            });
        }

        /// <summary>
        /// Add a global security requirement.
        /// </summary>
        /// <param name="swaggerGen">The swagger gen options.</param>
        /// <param name="id">The id name in open api reference.</param>
        private static void GenOpenApiSecurityRequirement(this SwaggerGenOptions swaggerGen, string id)
        {
            swaggerGen.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = id
                       }
                      },
                      new string[] { }
                    }
                  });
        }

        /// <summary>
        /// Setup Application Builder using Swagger and Swagger Ui.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void ConfigureUseSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Sent Di API V1");
            });
        }

    }
}
