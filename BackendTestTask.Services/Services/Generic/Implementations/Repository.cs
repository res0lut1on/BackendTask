using BackendTestTask.Database;
using BackendTestTask.Services.Services.Generic.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BackendTestTask.Services.Services.Generic.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly BackendTestTaskContext _context;

        public Repository(BackendTestTaskContext context)
        {
            _context = context;
        }

        public IQueryable<T> Query()
        {          
            return _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public List<TEntity> AddRange<TEntity>(List<TEntity> entities) where TEntity : class
        {
            _context.Set<TEntity>().AddRange(entities);
            _context.SaveChanges();
            return entities;
        }

        public async Task<List<TEntity>> AddRangeAsync<TEntity>(List<TEntity> entities) where TEntity : class
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public TEntity? Get<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return _context.Set<TEntity>().FirstOrDefault(filter);
        }

        public async Task<TEntity?> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>>? filter = null) where TEntity : class
        {
            if (filter == null)
            {
                return await _context.Set<TEntity>().AnyAsync();
            }
            else
            {
                return await _context.Set<TEntity>().AnyAsync(filter);
            }
        }
        public void Delete<TEntity>(int id) where TEntity : class, IEntity
        {
            var entity = _context.Set<TEntity>().Find(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public async Task DeleteAsync<TEntity>(int id) where TEntity : class, IEntity
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public void Delete<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            var entities = _context.Set<TEntity>().Where(filter).ToList();
            if (entities.Any())
            {
                _context.Set<TEntity>().RemoveRange(entities);
                _context.SaveChanges();
            }
        }

        public async Task DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            var entities = await _context.Set<TEntity>().Where(filter).ToListAsync();
            if (entities.Any())
            {
                _context.Set<TEntity>().RemoveRange(entities);
                await _context.SaveChangesAsync();
            }
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteRange<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            var entities = _context.Set<TEntity>().Where(filter).ToList();
            _context.Set<TEntity>().RemoveRange(entities);
            _context.SaveChanges();
        }

        public async Task DeleteRangeAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            var entities = await _context.Set<TEntity>().Where(filter).ToListAsync();
            _context.Set<TEntity>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public void DeleteRange<TEntity>(List<TEntity> entities) where TEntity : class
        {
            _context.Set<TEntity>().RemoveRange(entities);
            _context.SaveChanges();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }

}
