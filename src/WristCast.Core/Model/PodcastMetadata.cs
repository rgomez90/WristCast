using WristCast.Core.Data;

namespace WristCast.Core.Model
{
    public class PodcastMetadata : Entity<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
