using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Schaakcompetitie.Frontend.DAL.DataMappers
{
    public interface IDataMapper<T, Key>
    {
        T FindByName(T item);
        IEnumerable<T> FindAll();
        void Insert(T item);
        void Delete(T item);
        bool Exists(T item);
    }
}