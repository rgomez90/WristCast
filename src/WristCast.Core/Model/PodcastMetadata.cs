using SQLite;
using WristCast.Core.Data;

namespace WristCast.Core.Model
{
    public class PodcastMetadata : IEntity<string>
    {
        public PodcastMetadata() { }

        public PodcastMetadata(string id, string name, string publisher, string description)
        {
            Id = id;
            Name = name;
            Publisher = publisher;
            Description = description;
        }

        public string Name { get; set; }

        public string Publisher { get; set; }

        public string Description { get; set; }

        [PrimaryKey]
        public string Id { get; set; }
    }
}
