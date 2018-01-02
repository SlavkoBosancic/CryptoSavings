using CryptoSavings.Contracts.Repository;
using CryptoSavings.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.DAL.Repository
{
    internal class PurchaseRepository : LiteDBRepository<Purchase>, IPurchaseRepository
    {
        public IEnumerable<Purchase> GetPurchasesByUser(User user)
        {
            var result = new List<Purchase>();

            if(user != null)
            {
                var purchases = Get(x => x.User.Email == user.Email);
                result.AddRange(purchases);
            }

            return result;
        }
    }
}
