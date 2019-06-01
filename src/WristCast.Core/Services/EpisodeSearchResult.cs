using System;

namespace WristCast.Core.Services
{
    public class EpisodeSearchResult : ISearchResult
    {
        public string Audio { get; set; }

        public int AudioLengthSec { get; set; }

        public string Rss { get; set; }

        public string DescriptionHighlighted { get; set; }

        /// <summary>Plain text of this episode's description</summary>
        public string DescriptionOriginal { get; set; }

        /// <summary>Plain text of this episode' title</summary>
        public string TitleOriginal { get; set; }

        public string PodcastTitleOriginal { get; set; }

        public string PublisherOriginal { get; set; }

        public string Image { get; set; }

        public string Thumbnail { get; set; }

        public int ItunesId { get; set; }

        public DateTime PubDateMs { get; set; }

        public string Id { get; set; }

        public string PodcastId { get; set; }

        public bool ExplicitContent { get; set; }

        public bool IsDownloaded { get; set; }
    }
}