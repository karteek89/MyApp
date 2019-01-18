using System;
using System.Linq;
using MyApp.Entity.Poco;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;


namespace MyApp.Contract.Repository
{
    public interface IMyAppRepositoryAsync
    {
        Task<int> CountAsync<T>() where T : BaseEntity;

        Task<int> CountAsync<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity;

        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity;

        IQueryable<T> GetQueryAsync<T>() where T : class;

        int UpdateAsync<T>(T entity) where T : BaseEntity;

        int InsertAsync<T>(T entity) where T : BaseEntity;

        Task DeleteAsync<T>(T entity) where T : BaseEntity;

        void SetAdded(object entity);

    }
}
