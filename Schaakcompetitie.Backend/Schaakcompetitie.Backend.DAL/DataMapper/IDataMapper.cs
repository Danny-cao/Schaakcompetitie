using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Schaakcompetitie.Backend.DAL.DataMapper
{
    public interface IDataMapper<T, Key>
    {
        T Find(Key key);
        T FindByName(string key);
        IEnumerable<T> FindAll();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> clause);
        void Insert(T item);
        void Update(T item);
        void Delete(Key key);
        bool Exists(string item);
    }
}