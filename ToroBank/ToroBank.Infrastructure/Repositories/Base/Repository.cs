using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ToroBank.Core.Repositories.Interfaces.Base;
using ToroBank.Infrastructure.Context;

namespace ToroBank.Infrastructure.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly BaseContext _context;

        public Repository(BaseContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();
            return query.Where(predicate);
        }


        public void DetachAllEntities()
        {
            var changedEntriesCopy = _context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified ||
                            e.State == EntityState.Unchanged ||
                            e.State == EntityState.Added ||
                            e.State == EntityState.Detached)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await Task.FromResult(_context.Set<TEntity>().AsNoTracking().ToList());
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await Task.FromResult(_context.Set<TEntity>().Find(id));
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await CommitAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await CommitAsync();
            return entity;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            DetachAllEntities();
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            var entity = _context.Set<TEntity>().Find(id);
            _context.Set<TEntity>().Remove(entity);
            await CommitAsync();
            return new TEntity();
        }
    }
}
