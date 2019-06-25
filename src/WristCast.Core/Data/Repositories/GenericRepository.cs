using System.Collections.Generic;
using System.Threading.Tasks;

namespace WristCast.Core.Data.Repositories
{
    public abstract class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : IEntity<TKey>, new()
    {
        private readonly WristCastContext _context;

        public GenericRepository(WristCastContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(TKey id)
        {
            return await _context.Table<T>().FirstOrDefaultAsync(x => x.Id.Equals(id)).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Table<T>().ToListAsync().ConfigureAwait(false);
        }

        public Task Add(T entity)
        {
            return _context.Database.InsertAsync(entity);
        }

        public Task AddRange(IEnumerable<T> entities)
        {
            return _context.Database.InsertAllAsync(entities, typeof(T));
        }

        public Task Remove(T entity)
        {
            return _context.Database.DeleteAsync(entity);
        }
    }
}