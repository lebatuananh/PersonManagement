using System;
using System.Collections.Generic;
using System.Linq;

namespace PesonManagement.Data.Interface
{
    using System.Linq.Expressions;

    public interface IRepository<T, K> where T : class
    {
        T FindById(K id, params Expression<Func<T, object>>[] includeProperties);

        T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        T Add(T entity);

        T Update(T entity);

        T Remove(T entity);

        K Remove(K id);

        void RemoveMultiple(List<T> entities);
    }
}