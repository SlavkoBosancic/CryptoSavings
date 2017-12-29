using System;

namespace CryptoSavings.Model.DAL.Repository
{
    public class PrimaryKeyAttribute : Attribute
    {
        public PrimaryKeyAttribute()
        {
            AutoAssigned = true;
        }

        public bool AutoAssigned { get; set; }
    }
}
