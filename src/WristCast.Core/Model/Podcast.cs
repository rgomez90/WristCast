using System.Collections.Generic;
using System.Linq;
using System.Text;
using ListenNotesSearch.NET.Models;

namespace WristCast.Core.Model
{
    public class Podcast
    {
        public Podcast(PodcastFull podcast)
        {
            Country=podcast.Country;
            Description=podcast.Description;
            Image=podcast.Image;
            Website=podcast.Website;
            ExplicitContent = podcast.ExplicitContent;
            RssFeed=podcast.Rss;
            Publisher=podcast.Publisher;
            Name = podcast.Title;
            Episodes = podcast.Episodes.Select(x => new PodcastEpisode(x));
        }

        public IEnumerable<PodcastEpisode> Episodes { get; }

        public string Publisher { get; set; }

        public bool ExplicitContent { get; set; }

        public string Website { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public string Country { get; set; }

        public string Name { get; }

        public string RssFeed{ get; }
    }
}
