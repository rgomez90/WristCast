using WristCast.Core.Data;

namespace WristCast.Core.Model
{
    public class PodcastMetadata : Entity<string>
    {
        public PodcastMetadata() { }

        public PodcastMetadata(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }
        public string Description { get; }
    }
}
