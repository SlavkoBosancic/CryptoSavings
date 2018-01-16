using CryptoSavings.Contracts.Core;
using CryptoSavings.Infrastructure.DI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CryptoSavings.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Initialize the DB Engine and required data
            var dataInitializationResult = InitializeApplicationData();
            if (!dataInitializationResult)
            {
                // exit if data can not be initialized
                Shutdown();
            }
        }

        private bool InitializeApplicationData()
        {
            var result = false;

            try
            {
                AutofacContainer ac = new AutofacContainer();
                var container = ac.BuildContainer();
                var appManagerObj = container.GetService(typeof(IApplicationManager));

                if (appManagerObj != null && appManagerObj is IApplicationManager)
                {
                    ((IApplicationManager)appManagerObj).DataInitialization();
                    result = ((IApplicationManager)appManagerObj).DataInitialized;
                }
            }
            catch
            {

            }

            return result;
        }
    }
}
