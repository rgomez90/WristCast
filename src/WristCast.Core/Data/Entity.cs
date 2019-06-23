using SQLite;

namespace WristCast.Core.Data
{
    public abstract class Entity<T>
    {
        [PrimaryKey]
        public T Id { get; set; }
    }
}