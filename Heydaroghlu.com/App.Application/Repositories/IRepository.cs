using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> FindById(int id);
        Task<TEntity> FindById(int id,bool tracking=true,params string[] includes);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate,bool tracking=true,params string[] includes);
        Task<IQueryable<TEntity>> GetAsyncQuery(Expression<Func<TEntity, bool>> expression, bool tracking = true, params string[] includes);
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, params string[] includes);
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, int count, bool tracking = true, params string[] includes);
        Task<bool> IsAny(Expression<Func<TEntity, bool>> expression);
        TEntity Get(Expression<Func<TEntity, bool>> expression, bool tracking = true, params string[] includes);
        Task InsertAsync(TEntity entity);
        Task InsertRangeAsync(List<TEntity> entities);
        Task Remove(Expression<Func<TEntity, bool>> expression, bool tracking = true, params string[] includes);
        Task RemoveAsync(int id);
        Task RemoveRange(Expression<Func<TEntity, bool>> expression, bool tracking = true, params string[] includes);
        Task RemoveRange(List<TEntity> entities);
        void Remove(int id);
    }
}
