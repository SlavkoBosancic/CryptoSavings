using CryptoSavings.Contracts.Repository;
using CryptoSavings.Model;
using System.Collections.Generic;

namespace CryptoSavings.DAL.Repository
{
    public class UserRepository : LiteDBRepository<User>, IUserRepository
    {
        #region [CTOR]

        public UserRepository()
        {
        }

        #endregion

        public IEnumerable<User> GetAllUsers()
        {
            return base.GetAll();
        }

        public object CreateUser(User entity)
        {

            return base.Create(entity);
        }

        #region [Private]

        #endregion
    }
}
