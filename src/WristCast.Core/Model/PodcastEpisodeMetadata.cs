using System;
using WristCast.Core.Data;

namespace WristCast.Core.Model
{
    public class PodcastEpisodeMetadata : Entity<string>
    {
        public PodcastEpisodeMetadata()
        {

        }

        public PodcastEpisodeMetadata(string title, DateTime date, string description)
        {
            Title = title;
            Date = date;
            Description = description;
        }

        public string Title { get; }
        public DateTime Date { get; }
        public string Description { get; }
    }
}