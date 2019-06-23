using SQLite;

namespace WristCast.Core.Data
{
    public abstract class Entity<T>
    {
        protected Entity() { }

        protected Entity(T id)
        {
            Id = id;
        }

        [PrimaryKey]
        public T Id { get; protected set; }
    }
}