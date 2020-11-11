using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YourChoice.Domain;
using System.Linq.Expressions;

namespace YourChoice.Api.Repositories.interfaces
{
    public interface IRepository
    {
        Task<TEntity> GetById<TEntity>(int id) where TEntity : BaseEntity;

        Task<IEnumerable<TEntity>> GetAll<TEntity>() where TEntity : BaseEntity;

        Task<IEnumerable<TEntity>> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity;

        Task<TEntity> Add<TEntity>(TEntity entity) where TEntity : BaseEntity;

        Task<TEntity> Remove<TEntity>(int id) where TEntity : BaseEntity;

        TEntity Update<TEntity>(TEntity entity) where TEntity : BaseEntity;

        Task<bool> SaveAll();
    }
}
