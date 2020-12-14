using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YourChoice.Domain;
using System.Linq.Expressions;
using YourChoice.Api.Infrastructure.Models;
using System.Linq;

namespace YourChoice.Api.Repositories.interfaces
{
    public interface IRepository
    {
        Task<TEntity> GetById<TEntity>(int id) where TEntity : class;

        Task<IEnumerable<TEntity>> GetAll<TEntity>() where TEntity : class;


        Task<IEnumerable<TEntity>> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        Task<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;

        Task<List<TEntity>> AddRange<TEntity>(List<TEntity> entities) where TEntity : class;

        Task<TEntity> Remove<TEntity>(int id) where TEntity : class;

        TEntity Update<TEntity>(TEntity entity) where TEntity : class;

        Task<bool> SaveAll();

        public Task<PaginatedResult<TDto>> GetPagedData<TEntity, TDto>(PagedRequest pagedRequest) where TEntity : class
                                                                                                    where TDto : class;


        public Task<PaginatedResult<TDto>> GetPagedDataWithAdditionalPredicate<TEntity, TDto>
        (PagedRequest pagedRequest, Expression<Func<TEntity, bool>> additionalPredicate) where TEntity : class where TDto : class;
    }
}
