using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculatorService.Models;
using Easy.Logger;
using Easy.Logger.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CalculatorService
{
    public class Startup
    {
        ILogService logService = Log4NetService.Instance;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IEasyLogger logger = logService.GetLogger<Program>();

            logger.Debug("Configuring Services");

            services.AddMvc(
                config => { config.Filters.Add(typeof(CustomExceptionFilter)); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            IEasyLogger logger = logService.GetLogger<Program>();

            logger.Debug("Configuring MVC and Starting Application");

            app.UseStatusCodePages();
            app.UseMvc();
        }
    }
}
