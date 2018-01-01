using CryptoSavings.Infrastructure.Contracts.Repository;
using CryptoSavings.Model;
using System;
using System.Linq.Expressions;

namespace CryptoSavings.DAL.Repository
{
    public class UserRepository : LiteDBRepository<User>, IUserRepository
    {
        #region [CTOR]

        public UserRepository()
        {
        }

        #endregion

        public object CreateUser(User user)
        {
            return Create(user);
        }

        public User GetSingleUser(Expression<Func<User, bool>> where)
        {
            return GetSingle(where);
        }

        public bool UserExists(Expression<Func<User, bool>> where)
        {
            return Exists(where);
        }

        #region [Private]

        #endregion
    }
}
