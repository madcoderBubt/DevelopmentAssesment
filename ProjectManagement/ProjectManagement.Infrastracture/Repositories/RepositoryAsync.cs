using ProjectManagement.Infrastracture.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Infrastracture.Interface;
using ProjectManagement.Core.Entities;
using ProjectManagement.Infrastracture.Context;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Infrastracture.Repositories
{
    public class RepositoryAsync<TEntity> : IRepositoryAsync<TEntity> where TEntity : EntityBase
    {
        protected readonly AppDbContext _dbContext;

        private readonly ILogger<RepositoryAsync<TEntity>> _logger;

        public RepositoryAsync(AppDbContext dbContext, ILogger<RepositoryAsync<TEntity>> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Queries
        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeString = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var x = await _dbContext.Set<TEntity>().FindAsync(id);
            return x;
        }
        #endregion

        #region Commands
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        Task<TEntity> IRepositoryAsync<TEntity>.UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        Task<TEntity> IRepositoryAsync<TEntity>.DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
