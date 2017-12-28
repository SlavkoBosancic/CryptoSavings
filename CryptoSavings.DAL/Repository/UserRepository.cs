using CryptoSavings.Contracts.Repository;
using CryptoSavings.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.DAL.Repository
{
    public class UserRepository : LiteDBRepository<User>, IUserRepository
    {
        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }

    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
    }
}
