using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using YourChoice.Api.Database;
using YourChoice.Domain;
using YourChoice.Api.Repositories.interfaces;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace YourChoice.Api.Repositories.Implementation
{
    public class Repository : IRepository
    {
        protected readonly DataBaseContext context;


        public Repository(DataBaseContext context)
        {
            this.context = context;
        }

        public async Task<TEntity> GetById<TEntity>(int id) where TEntity : BaseEntity
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll<TEntity>() where TEntity : BaseEntity
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Find<TEntity>(Expression<Func<TEntity, bool>> predicate)
         where TEntity : BaseEntity
        {
            return await context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> Add<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            await context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public async Task<TEntity> Remove<TEntity>(int id) where TEntity : BaseEntity
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                throw new Exception($"Object of type {typeof(TEntity)} with id { id } not found");
            }
            context.Set<TEntity>().Remove(entity);
            return entity;
        }

        public TEntity Update<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<bool> SaveAll()
        {
            return await context.SaveChangesAsync() >= 0;
        }

        public async Task<List<TEntity>> AddRange<TEntity>(List<TEntity> entities) where TEntity : BaseEntity
        {
            await context.Set<TEntity>().AddRangeAsync(entities);
            return entities;
        }

        public async Task<List<TEntity>> GetPagedItems<TEntity, Tkey>(int startIndex, int number, Expression<Func<TEntity, Tkey>> order) 
            where TEntity : BaseEntity
        {
            var result =  await context.Set<TEntity>().OrderBy(order).Skip((startIndex - 1) * number).Take(number).ToListAsync();

            return result;
        }
        public async Task<List<TEntity>> GetPagedItems<TEntity>(int startIndex, int number) where TEntity : BaseEntity
        {
            var result = await context.Set<TEntity>().Skip((startIndex - 1) * number).Take(number).ToListAsync();

            return result;
        }
        public async Task<List<TEntity>> GetPagedItems<TEntity>(int startIndex, int number, Expression<Func<TEntity, bool>> predicate)
            where TEntity : BaseEntity
        {
            var result = await context.Set<TEntity>().Skip((startIndex - 1) * number).Take(number).ToListAsync();

            return result;
        }
    }
}
