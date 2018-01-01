using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CryptoSavings.Infrastructure.Contracts.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Expression<Func<T, bool>> where);
        T GetSingle(Expression<Func<T, bool>> where);

        object Create(T entity);
        bool Delete(T entity);
        bool Update(T entity);

        int CountAll();
        int Count(Expression<Func<T, bool>> where);

        bool Exists(Expression<Func<T, bool>> where);
    }
}
