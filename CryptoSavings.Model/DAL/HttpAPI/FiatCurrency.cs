﻿namespace CryptoSavings.Model.DAL.HttpAPI
{
    public class FiatCurrency : Currency
    {
        public string Symbol { get; set; }    // $, €, £ etc.
    }
}
