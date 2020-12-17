using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YourChoice.Domain;
using System.Linq.Expressions;
using YourChoice.Api.Infrastructure.Models;
using System.Linq;

namespace YourChoice.Api.Repositories.Interfaces
{
    public interface IRepository
    {
        Task<TEntity> GetById<TEntity>(int id) where TEntity : class;

        Task<IEnumerable<TEntity>> GetAll<TEntity>() where TEntity : class;

        Task<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;

        Task<List<TEntity>> AddRange<TEntity>(List<TEntity> entities) where TEntity : class;

        Task<TEntity> Remove<TEntity>(int id) where TEntity : class;

        TEntity Update<TEntity>(TEntity entity) where TEntity : class;

        Task<bool> SaveAll();

        public Task<PaginatedResult<TDto>> GetPagedData<TEntity, TDto>(PagedRequest pagedRequest) where TEntity : class
                                                                                                    where TDto : class;

    }
}
