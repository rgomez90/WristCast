using SQLite;

namespace WristCast.Core.Data
{
    public interface  IEntity<T>
    {
        T Id { get;}
    }
}