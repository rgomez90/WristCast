using System;
using WristCast.Core.Data;

namespace WristCast.Core.Model
{
    public class PodcastEpisodeMetadata:Entity<string>
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}