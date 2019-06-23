using System.Collections.Generic;
using System.Threading.Tasks;

namespace WristCast.Core.Data.Repositories
{
    public interface IGenericRepository<T, in TKey> where T : Entity<TKey>
    {
        Task<T> GetByIdAsync(TKey id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        Task Remove(T entity);
    }
}