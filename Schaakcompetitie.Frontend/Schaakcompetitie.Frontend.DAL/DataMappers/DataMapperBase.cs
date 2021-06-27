using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Schaakcompetitie.Frontend.DAL.DataMappers
{
    public abstract class DataMapperBase<T, TKey> : IDataMapper<T, TKey>
        where T : class
    {
        protected readonly FrontendContext Context;

        protected DataMapperBase(FrontendContext context)
        {
            Context = context;
        }
        
        public abstract T FindByName(T item);
        protected abstract IQueryable<T> GetAll();
        protected abstract bool _Exists(T item);

        public bool Exists(T item)
        {
            return _Exists(item);
        }

        public IEnumerable<T> FindAll()
        {
            return GetAll().ToList();
        }

        public virtual void Insert(T item)
        {
            if (_Exists(item))
            {
                throw new DataMapperException($"cannot insert duplicate item<{item}>");
            }

            Context.Add(item);
            Context.SaveChanges();
        }

        public virtual void Delete(T item)
        {
            Context.Remove(item);
            Context.SaveChanges();
        }
    }
}