using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Schaakcompetitie.Backend.DAL.DataMapper
{
    public abstract class DataMapperBase<T, Key> : IDataMapper<T, Key> 
        where T : class
    {
        protected readonly SchaakContext Context;

        protected DataMapperBase(SchaakContext context)
        {
            Context = context;
        }

        public abstract T Find(Key key);
        public abstract T FindByName(string key);

        public abstract IEnumerable<T> FindAll();

        public abstract IEnumerable<T> FindBy(Expression<Func<T, bool>> clause);

        public abstract void Insert(T item);

        public abstract void Update(T item);

        public abstract void Delete(Key key);

        public abstract bool Exists(string item);
    }
}