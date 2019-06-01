namespace WristCast.Core.Services
{
    public static class Extensions
    {
        public static EpisodeSearchResult ToResult(this ListenNotesSearch.NET.Models.EpisodeSearchResult result)
        {
            return new EpisodeSearchResult()
            {
                Audio = result.Audio,
                AudioLengthSec = result.AudioLengthSec,
                Rss = result.Audio,
                DescriptionHighlighted = result.DescriptionHighlighted,
                TitleOriginal = result.TitleOriginal,
                DescriptionOriginal = result.DescriptionOriginal,
                PodcastTitleOriginal = result.PodcastTitleOriginal,
                PublisherOriginal = result.PublisherOriginal,
                Image = result.Image,
                Thumbnail = result.Thumbnail,
                Id = result.Id,
                ItunesId = result.ItunesId,
                PodcastId = result.PodcastId,
                PubDateMs = result.PubDateMs,
                ExplicitContent = result.ExplicitContent
            };
        }

        public static PodcastSearchResult ToResult(this ListenNotesSearch.NET.Models.PodcastSearchResult result)
        {
            return new PodcastSearchResult()
            {
                Rss = result.Rss,
                DescriptionHighlighted = result.DescriptionHighlighted,
                TitleOriginal = result.TitleOriginal,
                DescriptionOriginal = result.DescriptionOriginal,
                PublisherOriginal = result.PublisherOriginal,
                Image = result.Image,
                Thumbnail = result.Thumbnail,
                Id = result.Id,
                ItunesId = result.ItunesId,
                LatestPubDateMs = result.LatestPubDateMs,
                EarliestPubDateMs = result.EarliestPubDateMs,
                ExplicitContent = result.ExplicitContent,
                Email = result.Email,
                TotalEpisodes = result.TotalEpisodes
            };
        }
    }
}