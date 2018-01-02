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

namespace CryptoSavings.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var dataInitializationResult = InitializeData();
            if (!dataInitializationResult)
            {
                Console.WriteLine("Required data could not be initialized. Press any key to terminate...");
                return;
            }

            var webHost = WebHost.CreateDefaultBuilder(args)
                                 .UseStartup<Startup>()
                                 .UseKestrel()
                                 .Build();

            // start the web host
            webHost.Run();
        }

        private static bool InitializeData()
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
