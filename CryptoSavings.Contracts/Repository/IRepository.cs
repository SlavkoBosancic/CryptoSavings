using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.Contracts.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Func<T, bool> where);
        T GetSingle(Func<T, bool> where);

        object Create(T entity);
        bool Delete(T entity);
        bool Update(T entity);
    }
}
