using System;
using System.Collections.Generic;
using System.Linq;

namespace PesonManagement.Data.Implementation
{
    using Microsoft.EntityFrameworkCore;
    using PesonManagement.Data.Entity.Interface;
    using PesonManagement.Data.Interface;
    using PesonManagement.Utils;
    using System.Linq.Expressions;

    public class EFRepository<T, K> : IRepository<T, K>, IDisposable where T : DomainEntity<K>
    {
        private readonly AppDbContext _context;

        public EFRepository(AppDbContext context)
        {
            this._context = context;
        }

        public T FindById(K id, params Expression<Func<T, object>>[] includeProperties)
        {
            return this.FindAll(includeProperties).SingleOrDefault(x => x.Id.Equals(id));
        }

        public T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return this.FindAll(includeProperties).SingleOrDefault(predicate);
        }

        public IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = this._context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }

            return items;
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = this._context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }

            return items.Where(predicate);
        }

        public T Add(T entity)
        {
            this._context.Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            var dbEntity = _context.Set<T>().AsNoTracking().Single(p => p.Id.Equals(entity.Id));
            var databaseEntry = _context.Entry(dbEntity);
            var inputEntry = _context.Entry(entity);

            //no items mentioned, so find out the updated entries
            IEnumerable<string> dateProperties = typeof(IDateTracking).GetPublicProperties().Select(x => x.Name);

            var allProperties = databaseEntry.Metadata.GetProperties()
                .Where(x => !dateProperties.Contains(x.Name));

            foreach (var property in allProperties)
            {
                var proposedValue = inputEntry.Property(property.Name).CurrentValue;
                var originalValue = databaseEntry.Property(property.Name).OriginalValue;

                if (proposedValue != null && !proposedValue.Equals(originalValue))
                {
                    databaseEntry.Property(property.Name).IsModified = true;
                    databaseEntry.Property(property.Name).CurrentValue = proposedValue;
                }
            }

            var result = _context.Set<T>().Update(dbEntity);
            return result.Entity;
        }

        public T Remove(T entity)
        {
            this._context.Set<T>().Remove(entity);
            return entity;
        }

        public K Remove(K id)
        {
            var entity = FindById(id);
            Remove(entity);
            return id;
        }

        public void RemoveMultiple(List<T> entities)
        {
            this._context.Set<T>().RemoveRange(entities);
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}