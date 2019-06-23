using System.Collections.Generic;
using System.Linq;
using ListenNotesSearch.NET.Models;

namespace WristCast.Core.Model
{
    public class Podcast
    {
        public Podcast(PodcastFull podcast)
        {
            Country = podcast.Country;
            Description = podcast.Description;
            Image = podcast.Image;
            Website = podcast.Website;
            ExplicitContent = podcast.ExplicitContent;
            RssFeed = podcast.Rss;
            Publisher = podcast.Publisher;
            Name = podcast.Title;
            Episodes = podcast.Episodes.Select(x => new PodcastEpisode(x));
        }

        public Podcast(PodcastSimple podcast, IEnumerable<PodcastEpisode> episodes)
        {
            Country = podcast.Country;
            Description = podcast.Description;
            Image = podcast.Image;
            Website = podcast.Website;
            ExplicitContent = podcast.ExplicitContent;
            RssFeed = podcast.Rss;
            Publisher = podcast.Publisher;
            Name = podcast.Title;
            Episodes = episodes;
            Id = podcast.Id;
        }

        public string Id { get; set; }

        public string Publisher { get; set; }

        public bool ExplicitContent { get; set; }

        public string Website { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public string Country { get; set; }

        public string Name { get; }

        public string RssFeed { get; }

        public IEnumerable<PodcastEpisode> Episodes { get; }

        public bool IsSubscribed { get; set; }

        public PodcastMetadata GetMetadata()
        {
            return new PodcastMetadata(Id, Name, Description);
        }
    }
}
