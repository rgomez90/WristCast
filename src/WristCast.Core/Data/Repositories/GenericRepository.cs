using System.Collections.Generic;
using System.Threading.Tasks;

namespace WristCast.Core.Data.Repositories
{
    public abstract class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : Entity<TKey>, new()
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
    }
}