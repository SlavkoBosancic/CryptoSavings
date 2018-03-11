using CryptoSavings.Contracts.Core;
using CryptoSavings.Infrastructure.DI;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace CryptoSavings.Angular
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Initialize the DB Engine and required data
            var dataInitializationResult = InitializeApplicationData();
            if (!dataInitializationResult)
            {
                Console.WriteLine("Required data could not be initialized. Press any key to terminate...");
                Console.ReadKey();
                return;
            }

            var webHost = WebHost.CreateDefaultBuilder(args)
                                 .UseStartup<Startup>()
                                 .Build();

            // start the web host
            webHost.Run();
        }

        private static bool InitializeApplicationData()
        {
            var result = false;

            AutofacContainer ac = new AutofacContainer();
            var container = ac.BuildContainer();
            var appManagerObj = container.GetService(typeof(IApplicationManager));

            if (appManagerObj != null && appManagerObj is IApplicationManager)
            {
                ((IApplicationManager)appManagerObj).DataInitialization();
                result = ((IApplicationManager)appManagerObj).DataInitialized;
            }

            return result;
        }
    }
}
