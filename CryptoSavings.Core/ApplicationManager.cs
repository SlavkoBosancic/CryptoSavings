using CryptoSavings.Contracts.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.Core
{
    public class ApplicationManager : IApplicationManager
    {
        private bool _dataInitialized;
        private static readonly object _dataInitializationLock = new object();

        #region [CTOR]

        public ApplicationManager()
        {

        }

        #endregion

        public bool DataInitialized => _dataInitialized;

        public void DataInitialization()
        {
            if (!_dataInitialized)
            {
                lock (_dataInitializationLock)
                {
                    if (!_dataInitialized)
                    {
                        // check if Fiat currencies loaded
                            // load them from config

                        // check if DB has currencies loaded
                            //load them from http api

                        // check if DB has exchanges
                            // load them from http api

                        // create first demo user in user DB
                    }
                }
            }

        }

        #region [Private]

        #endregion
    }
}
