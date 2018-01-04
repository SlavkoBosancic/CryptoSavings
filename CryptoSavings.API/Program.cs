using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CryptoSavings.Infrastructure.DI;
using CryptoSavings.Contracts.Core;
using System.Net;

namespace CryptoSavings.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dataInitializationResult = InitializeApplicationData();
            if (!dataInitializationResult)
            {
                Console.WriteLine("Required data could not be initialized. Press any key to terminate...");
                Console.ReadKey();
                return;
            }

            var webHost = WebHost.CreateDefaultBuilder(args)
                                 .UseStartup<Startup>()
                                 .UseUrls("http://localhost:9999")
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

            if(appManagerObj != null && appManagerObj is IApplicationManager)
            {
                ((IApplicationManager)appManagerObj).DataInitialization();
                result = ((IApplicationManager)appManagerObj).DataInitialized;
            }

            return result;
        }
    }
}
