using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using YourChoice.Api.Database;
using YourChoice.Domain;
using YourChoice.Api.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using YourChoice.Api.Infrastructure.Models;
using YourChoice.Api.Infrastructure.Extensions;
using AutoMapper;

namespace YourChoice.Api.Repositories.Implementation
{
    public class Repository : IRepository
    {
        protected readonly DataBaseContext context;
        protected readonly IMapper mapper;

        public Repository(DataBaseContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TEntity> GetById<TEntity>(int id) where TEntity : class
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll<TEntity>() where TEntity : class
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Add<TEntity>(TEntity entity) where TEntity : class
        {
            await context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public async Task<TEntity> Remove<TEntity>(int id) where TEntity : class
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                throw new Exception($"Object of type {typeof(TEntity)} with id { id } not found");
            }
            context.Set<TEntity>().Remove(entity);
            return entity;
        }

      
        public TEntity Update<TEntity>(TEntity entity) where TEntity : class
        {
            context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<bool> SaveAll()
        {
            return await context.SaveChangesAsync() >= 0;
        }

        public async Task<List<TEntity>> AddRange<TEntity>(List<TEntity> entities) where TEntity : class
        {
            await context.Set<TEntity>().AddRangeAsync(entities);
            return entities;
        }

        public async Task<PaginatedResult<TDto>> GetPagedData<TEntity, TDto>(PagedRequest pagedRequest) where TEntity : class
                                                                                                    where TDto : class
        {

            return await context.Set<TEntity>().CreatePaginatedResultAsync<TEntity, TDto>(pagedRequest, mapper);
        }

        

    }
}
