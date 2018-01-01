using CryptoSavings.Infrastructure.Contracts.Repository;
using CryptoSavings.Model;
using System.Collections.Generic;

namespace CryptoSavings.DAL.Repository
{
    public class PurchaseRepository : LiteDBRepository<Purchase>, IPurchaseRepository
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
