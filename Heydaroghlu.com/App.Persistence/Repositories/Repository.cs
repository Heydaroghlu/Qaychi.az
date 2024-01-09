using App.Application.Repositories;
using App.Domain.Base;
using App.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace App.Persistence.Repositories
{


    public class Repository<TEntity>: IRepository<TEntity> where TEntity : class
    {

        readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }
        DbSet<TEntity> Table
        {
            get=>_context.Set<TEntity>();
            set => _context.Set<TEntity>();
        }


        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, params string[] includes)
        {
            var query = Table.Where(expression);
            query = IsTracking(query, tracking);
            query = Includes(query, includes);
            //await query.LoadAsync();
            return query;
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, int count, bool tracking = true, params string[] includes)
        {
            var query = Table.Where(expression);
            query = IsTracking(query, tracking);
            query = Includes(query, includes);
            return query;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, params string[] includes)
        {
            var query = Table.AsQueryable<TEntity>();
            query = IsTracking(query, tracking);
            query = Includes(query, includes);
            //await query.LoadAsync();

            var entity = await query.FirstOrDefaultAsync(expression);

            return entity;
        }

        public async Task<IQueryable<TEntity>> GetAsyncQuery(Expression<Func<TEntity, bool>> expression, bool tracking = true, params string[] includes)
        {
            var query = Table.AsQueryable<TEntity>().Where(expression);
            query = IsTracking(query, tracking);
            query = Includes(query, includes);


            return query;
        }

        public async Task InsertAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
        }

        public async Task InsertRangeAsync(List<TEntity> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public async Task<bool> IsAny(Expression<Func<TEntity, bool>> expression)
        {
            return await Table.AnyAsync(expression);
        }

        public async Task Remove(Expression<Func<TEntity, bool>> expression, bool tracking = true, params string[] includes)
        {
            var entity = await Table.FirstOrDefaultAsync(expression);
            Table.Remove(entity);
        }
        public async Task RemoveAsync(int id)
        {
            var entity = await Table.FindAsync(id);
            Table.Remove(entity);
        }

        public async Task RemoveRange(Expression<Func<TEntity, bool>> expression, bool tracking = true, params string[] includes)
        {
            var entities = Table.Where(expression).ToList();
            Table.RemoveRange(entities);
        }

        private IQueryable<TEntity> IsTracking(IQueryable<TEntity> query, bool tracking)
        {
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return query;

        }
        private IQueryable<TEntity> Includes(IQueryable<TEntity> query, params string[] includes)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query;
        }

        public async Task RemoveRange(List<TEntity> entities)
        {
            Table.RemoveRange(entities);
        }

        public void Remove(int id)
        {
            var entity = Table.Find(id);
            Table.Remove(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> expression, bool tracking = true, params string[] includes)
        {
            var query = Table.AsQueryable<TEntity>();
            query = IsTracking(query, tracking);
            query = Includes(query, includes);

            var entity = query.FirstOrDefault(expression);

            return entity;
        }

        public Task<TEntity> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindById(int id, bool tracking = true, params string[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
