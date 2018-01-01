using Autofac;
using Autofac.Extensions.DependencyInjection;
using CryptoSavings.Contracts.Core;
using CryptoSavings.Contracts.Repository;
using CryptoSavings.Core;
using CryptoSavings.DAL.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.Infrastructure.DI
{
    public class AutofacContainer
    {
        public IServiceProvider BuildContainer(IServiceCollection populateService = null)
        {
            var containerBuilder = new ContainerBuilder();

            if (populateService != null)
            {
                // Everything registered in WebAPI Startup ServiceCollection should be
                // Populated to bring those registrations into Autofac. This is
                // just like a foreach over the list of things in the collection
                // to add them to Autofac.
                containerBuilder.Populate(populateService);
            }

            // Make your Autofac registrations. Order is important!
            // If you make them BEFORE you call Populate, then the
            // registrations in the ServiceCollection will override Autofac
            // registrations; if you make them AFTER Populate, the Autofac
            // registrations will override. You can make registrations
            // before or after Populate, however you choose.

            containerBuilder.RegisterType<PortfolioManager>().As<IPortfolioManager>();
            containerBuilder.RegisterType<PurchaseRepository>().As<IPurchaseRepository>();

            // Creating a new AutofacServiceProvider makes the container
            // available to your app using the Microsoft IServiceProvider
            // interface so you can use those abstractions rather than
            // binding directly to Autofac.
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}
