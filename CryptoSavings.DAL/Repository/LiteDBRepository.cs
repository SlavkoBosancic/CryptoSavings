using CryptoSavings.Contracts.Repository;
using CryptoSavings.Model.DAL.Repository;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace CryptoSavings.DAL.Repository
{
    /// <summary>
    /// This non-generic base class is needed in order to have a singleton LiteDatabase instance
    /// as recommended in scenarios where thread-safety is needed but not proces-safety.
    /// Creatina a new instance for each of the Repository<T> would be unnecessary slow.
    /// </summary>
    internal abstract class LiteDBRepository
    {
        protected static readonly string _internalIdPropertyName = "_id";
        protected static readonly LiteDatabase _db = new LiteDatabase("CryptoSavings.Repository.db");

        #region [CTOR]

        static LiteDBRepository()
        {
            // Assign the primary-key property name in the global mapper of LiteDB
            _db.Mapper.ResolveMember = PrimaryKeyMapper;
        }

        #endregion

        private static void PrimaryKeyMapper(Type memberType, MemberInfo memberInfo, MemberMapper mMapper)
        {
            var key = memberInfo.GetCustomAttribute<PrimaryKeyAttribute>(true);
            if (key != null)
            {
                mMapper.AutoId = key.AutoAssigned;
                mMapper.FieldName = _internalIdPropertyName;
            }
        }
    }

    internal class LiteDBRepository<T> : LiteDBRepository, IRepository<T> where T : class, new()
    {
        private static readonly string _tableName;
        private static readonly string _keyPropertyName = string.Empty;
        private static readonly bool _keyPropertyAutoAssigned = true;

        #region [CTOR]

        static LiteDBRepository()
        {
            // Set table/collection name
            _tableName = typeof(T).Name;

            // Set primary-key column name
            var props = typeof(T).GetRuntimeProperties();
            foreach(var prop in props)
            {
                var keyAttribute = prop.GetCustomAttribute<PrimaryKeyAttribute>(true);
                if(keyAttribute != null)
                {
                    _keyPropertyAutoAssigned = keyAttribute.AutoAssigned;
                    _keyPropertyName = prop.Name;

                    break;
                }
            }

            // if no custom Primary key attribute found, use LiteDB rules for Id detection
            if (string.IsNullOrEmpty(_keyPropertyName))
            {
                var idProp = props.FirstOrDefault(x => x.Name == "Id");

                if(idProp == null)
                {
                    idProp = props.FirstOrDefault(x => x.Name == string.Format("{0}Id", _tableName));
                }

                if(idProp != null)
                {
                    _keyPropertyAutoAssigned = true;
                    _keyPropertyName = idProp.Name;
                }
            }
        }

        #endregion

        public object Create(T entity)
        {
            object result = null;

            if (entity != null)
            {
                if (!_keyPropertyAutoAssigned)
                {
                    var keyValue = GetKeyPropertyValue(entity);
                    var exists = _db.GetCollection<T>()
                                    .Exists(Query.EQ(_internalIdPropertyName, new BsonValue(keyValue)));

                    if (exists)
                        return null;
                }
                else
                {
                    ClearIdPropertyValue(entity);
                }

                var newKey = _db.GetCollection<T>().Insert(entity);
                result = newKey != BsonValue.Null ? newKey.RawValue : null;
            }

            return result;
        }

        public bool Delete(T entity)
        {
            var result = false;

            if (entity != null)
            {
                var keyValue = GetKeyPropertyValue(entity);

                if (keyValue != null)
                {
                    result = _db.GetCollection<T>()
                                .Delete(new BsonValue(keyValue));
                }
            }

            return result;
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> where)
        {
            return _db.GetCollection<T>()
                      .Find(where);
        }

        public IEnumerable<T> GetAll()
        {
            return _db.GetCollection<T>()
                      .FindAll();
        }

        public T GetSingle(Expression<Func<T, bool>> where)
        {
            return _db.GetCollection<T>()
                      .FindOne(where);
        }

        public bool Update(T entity)
        {
            var result = false;

            if(entity != null)
            {
                result = _db.GetCollection<T>()
                            .Update(entity);
            }

            return result;
        }

        public int CountAll()
        {
            return _db.GetCollection<T>()
                      .Count();
        }

        public int Count(Expression<Func<T, bool>> where)
        {
            return _db.GetCollection<T>()
                      .Count(where);
        }

        public bool Exists(Expression<Func<T, bool>> where)
        {
            return _db.GetCollection<T>()
                      .Exists(where);
        }

        #region [Private]

        private object GetKeyPropertyValue(T entity)
        {
            object result = null;

            if (entity != null && !string.IsNullOrEmpty(_keyPropertyName))
            {
                result = typeof(T).GetProperty(_keyPropertyName)
                                  .GetValue(entity);
            }

            return result;
        }

        private void ClearIdPropertyValue(T entity)
        {
            if (entity != null && !string.IsNullOrEmpty(_keyPropertyName))
            {
                typeof(T).GetProperty(_keyPropertyName)
                         .SetValue(entity, null);
            }
        }

        #endregion
    }
}
