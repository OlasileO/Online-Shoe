using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OnlineShoe.Model.Data;
using OnlineShoe.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoe.Repository.Implementation
{
    public class GenericRepository<T> : IDisposable, IGenericRepository<T> where T : class
    {
        private readonly ShoeDbContext _Context;

        public GenericRepository(ShoeDbContext Context)
        {
            _Context = Context;
        }
        private bool disposed = true;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                if (disposing)
                {
                    _Context.Dispose();
                }
            }
            this.disposed = true;
        }
        public async Task<T> AddAsync(T entity)
        {
           await _Context.Set<T>().AddAsync(entity);
            await _Context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = await _Context.Set<T>().FindAsync(id);
            _Context.Set<T>().Remove(entity);
            await _Context.SaveChangesAsync();
            return entity;

        }

        public void DeleteRange(List<T> entity)
        {
            _Context.Set<T>().RemoveRange(entity);
        }

        public async Task<bool> Exits(int id)
        {
            var entity = await GetById(id);
            return entity != null;

        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _Context.Set<T>();
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (include != null)
            {
                query = include(query);
            }
            if (orderby != null)
            {
                query = orderby(query);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            var result = await _Context.Set<T>().FindAsync(id);
            return result;
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _Context.Set<T>();
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (include != null)
            {
                query = include(query);
            }
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }


        public async Task<T> UpdateAsync(T entity)
        {
            _Context.Set<T>().Attach(entity);
            _Context.Entry(entity).State = EntityState.Modified;
            _Context.SaveChanges();
            return entity;
        }
    }
}
