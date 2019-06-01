using System;

namespace WristCast.Core.Services
{
    public class PodcastSearchResult:ISearchResult
    {
        public string Rss { get; set; }

        /// <summary>Highlighted segment of podcast description</summary>
        public string DescriptionHighlighted { get; set; }

        /// <summary>Plain text of podcast description</summary>
        public string DescriptionOriginal { get; set; }

        public string TitleOriginal { get; set; }

        public string PublisherOriginal { get; set; }

        public string Image { get; set; }

        public string Thumbnail { get; set; }

        public int ItunesId { get; set; }

        public DateTime LatestPubDateMs { get; set; }
        
        public DateTime EarliestPubDateMs { get; set; }

        public string Id { get; set; }
       
        public int TotalEpisodes { get; set; }

        public string Email { get; set; }

        public bool ExplicitContent { get; set; }
    }
}