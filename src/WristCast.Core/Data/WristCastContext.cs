using System.Threading.Tasks;
using SQLite;
using WristCast.Core.Model;

namespace WristCast.Core.Data
{
    public class WristCastContext
    {
        // SQLite connection
        private readonly SQLiteAsyncConnection database;

        public WristCastContext(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
        }

        public AsyncTableQuery<T> Table<T>() where T : new()
        {
            return database.Table<T>();
        }

        public SQLiteAsyncConnection Database => database;

        public AsyncTableQuery<PodcastMetadata> PodcastMetadata => database.Table<PodcastMetadata>();

        public AsyncTableQuery<PodcastEpisodeMetadata> PodcastEpisodeMetadata => database.Table<PodcastEpisodeMetadata>();

        public Task Create()
        {
            var tasks = new[]
            {
                database.CreateTableAsync<PodcastEpisodeMetadata>(),
                database.CreateTableAsync<PodcastMetadata>()
            };
            return Task.WhenAll(tasks);
        }
    }
}
