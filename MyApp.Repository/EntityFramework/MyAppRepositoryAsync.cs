using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Contract.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using MyApp.Entity.Poco;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using MyApp.Configuration;

namespace MyApp.Repository.EntityFramework
{
    public class MyAppRepositoryAsync : IMyAppRepositoryAsync
    {
        protected readonly DbContext _dbContext;

        public MyAppRepositoryAsync()
        {
            _dbContext = new MyAppContext();
        }

        public virtual Task<int> CountAsync<T>() where T : BaseEntity
        {
            var c = _dbContext.Set<T>().Count();
            return Task.FromResult(c);
        }

        public virtual Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity
        {
            var c = _dbContext.Set<T>().Count(predicate);
            return Task.FromResult(c);
        }

        public virtual Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity
        {
            var result = _dbContext.Set<T>().FirstOrDefault(predicate);
            return Task.FromResult(result);
        }

        public virtual IQueryable<T> GetQueryAsync<T>() where T : class
        {
            return _dbContext.Set<T>();
        }

        public virtual int UpdateAsync<T>(T entity) where T : BaseEntity
        {
            var result = 0;
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                result = _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                result = -1;
            }
            return result;
        }

        public virtual int InsertAsync<T>(T entity) where T : BaseEntity
        {
            entity.CreatedOn = DateTime.Now;

            SetAdded(entity);
            _dbContext.Set<T>().Add(entity);

            return _dbContext.SaveChanges();
        }

        public virtual void SetAdded(object entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
        }

        public virtual Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            // entity.ModifiedDate = DateTime.Now;
            _dbContext.Entry(entity).State = EntityState.Deleted;
            return Task.FromResult(_dbContext.SaveChangesAsync());
        }

    }
}
