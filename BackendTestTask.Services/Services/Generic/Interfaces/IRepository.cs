using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Services.Generic.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query();
        Task<T> GetByIdAsync(object id);
        TEntity Add<TEntity>(TEntity entity) where TEntity : class;
        Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class;
        List<TEntity> AddRange<TEntity>(List<TEntity> entities) where TEntity : class;
        Task<List<TEntity>> AddRangeAsync<TEntity>(List<TEntity> entities) where TEntity : class; void Update(T entity);
        void Delete(T entity);
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;
        TEntity? Get<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;
        Task<TEntity?> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;
        Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>>? filter = null) where TEntity : class;

        Task<int> SaveAsync();
    }

}
