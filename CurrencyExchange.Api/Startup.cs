using System;
using CurrencyExchange.Api.Extentions;
using CurrencyExchange.Api.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyExchange.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            NLog.LogManager.LoadConfiguration(string.Concat(System.IO.Directory.GetCurrentDirectory(), "/NLog.config"));
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            services.ConfigureDataLayer();
            services.ConfigureBusniessLayer();
            services.ConfigureProduceResponseType();
            services.ConfigureComponent();
            services.ConfigureCors();
            services.ConfigureSwagger();
            services.ConfigureMvc();
            services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (string.Equals(env.EnvironmentName, "Development", StringComparison.OrdinalIgnoreCase))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.ConfigureUseSwagger();
            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();
            app.UseMiddleware<Middleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
