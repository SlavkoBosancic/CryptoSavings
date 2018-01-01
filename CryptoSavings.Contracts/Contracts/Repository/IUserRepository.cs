using CryptoSavings.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CryptoSavings.Infrastructure.Contracts.Repository
{
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieve single of first User using an expression.
        /// </summary>
        /// <param name="where">Where expression.</param>
        /// <returns>User object or null if not found.</returns>
        User GetSingleUser(Expression<Func<User, bool>> where);

        /// <summary>
        /// Inserts a user instance into the repository and returns a key object.
        /// </summary>
        /// <param name="user">User object to be persisted.</param>
        /// <returns>Key object for the newly inserted object or null if already exsisting or other failure.</returns>
        object CreateUser(User user);

        /// <summary>
        /// Fast check if user(s) already persisted in the repository.
        /// </summary>
        /// <param name="where">Where expression.</param>
        /// <returns>True of false if found.</returns>
        bool UserExists(Expression<Func<User, bool>> where);
    }
}
