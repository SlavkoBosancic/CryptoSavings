using CryptoSavings.Contracts.Repository;
using CryptoSavings.Model;
using CryptoSavings.Model.DAL.HttpAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.DAL.Repository
{
    internal class PurchaseRepository : LiteDBRepository<Purchase>, IPurchaseRepository
    {
        #region [CTOR]

        static PurchaseRepository()
        {
            _db.Mapper.Entity<Purchase>()
                      .DbRef(x => x.Exchange, nameof(Exchange))
                      .DbRef(x => x.User, nameof(User));
        }

        #endregion

        public IEnumerable<Purchase> GetPurchasesByUser(string userId, bool includeReferences = false)
        {
            var result = new List<Purchase>();

            if(!string.IsNullOrEmpty(userId))
            {
                IEnumerable<Purchase> userPurchases = null;

                if (includeReferences)
                {
                    userPurchases = _db.GetCollection<Purchase>()
                                       .Include<Exchange>(x => x.Exchange)
                                       .Include<User>(x => x.User)
                                       .Find(x => x.User.Email == userId);
                }
                else
                {
                    userPurchases = Get(x => x.User.Email == userId);
                }

                result.AddRange(userPurchases);
            }

            return result;
        }
    }
}
