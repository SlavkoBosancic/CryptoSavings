using CryptoSavings.Contracts.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoSavings.DAL.Repository
{
    public class LiteDBRepository<T> : IRepository<T> where T : class
    {
        public object Create(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get(Func<T, bool> where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetSingle(Func<T, bool> where)
        {
            throw new NotImplementedException();
        }

        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
