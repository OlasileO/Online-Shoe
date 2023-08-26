using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace OnlineShoe.Repository.Abstract
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null,
                   Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
                   Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
                     , bool disableTracking = true);

        Task<T> GetByIdAsync(Expression<Func<T, bool>> filter = null,
                         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                         bool disableTracking = true);
        Task<T> GetById(int id);

        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);
        void DeleteRange(List<T> entity);
        Task<bool> Exits(int id);
    }
}
