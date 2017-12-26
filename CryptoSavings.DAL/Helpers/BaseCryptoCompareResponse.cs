using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.DAL.Helpers
{
    class BaseCryptoCompareResponse
    {
        public string Response { get; set; }
        public int Type { get; set; }
        public dynamic Data { get; set; }
    }
}
