using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CryptoSavings.Infrastructure.DI;

namespace CryptoSavings.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        #region [CTOR]

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        // Dependancy and services configuration
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Adding MVC (including WebAPI)
            services.AddMvc();

            AutofacContainer container = new AutofacContainer();
            return container.BuildContainer(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug(LogLevel.Debug);
            loggerFactory.AddConsole(LogLevel.Debug);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error.html");
            }

            app.UseFileServer(new FileServerOptions { EnableDefaultFiles = true, EnableDirectoryBrowsing = false });
            app.UseMvc();
        }
    }
}
