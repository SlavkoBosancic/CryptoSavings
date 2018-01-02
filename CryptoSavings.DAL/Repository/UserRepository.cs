using CryptoSavings.Contracts.Repository;
using CryptoSavings.Model;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;

namespace CryptoSavings.DAL.Repository
{
    internal class UserRepository : LiteDBRepository<User>, IUserRepository
    {

    }
}
