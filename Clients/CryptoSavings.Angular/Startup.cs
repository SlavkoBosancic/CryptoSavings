using CryptoSavings.Infrastructure.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CryptoSavings.Angular
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

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            // Adding asp.net services provider to global Autofac DI container
            AutofacContainer container = new AutofacContainer();
            return container.BuildContainer(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            appLifetime.ApplicationStarted.Register(this.ApplicationStarted);
            appLifetime.ApplicationStopping.Register(this.ApplicationStopping);
            appLifetime.ApplicationStopped.Register(this.ApplicationStopped);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error.html");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMvc();

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        private void ApplicationStarted()
        {

        }

        private void ApplicationStopping()
        {

        }

        private void ApplicationStopped()
        {
            // cleanup
            // stop DB engine
            // etc.
        }
    }
}
