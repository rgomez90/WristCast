using System;
using ListenNotesSearch.NET.Models;

namespace WristCast.Core.Model
{
    public class PodcastEpisode
    {
        public PodcastEpisode(IEpisode episode)
        {
            Title = episode.Title;
            Description = episode.Description;
            Image = episode.Image;
            Audio = episode.Audio;
            AudioLength = TimeSpan.FromSeconds(episode.AudioLengthSec);
            PublishDate = episode.PubDateMs;
            ExplicitContent = episode.ExplicitContent;
            Id = episode.Id;
        }

        public bool IsDownloaded { get; set; }

        public string Id { get; set; }

        public DateTime PublishDate { get; set; }

        public TimeSpan AudioLength { get; set; }

        public string Title { get; set; }

        public string Audio { get; set; }

        public string Publisher { get; set; }

        public bool ExplicitContent { get; set; }

        public string Website { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public string DescriptionShort => Description.Length < 35 ? Description : $"{Description.Substring(0, 35)}...";

        public Podcast Podcast { get; internal set; }

        public PodcastEpisodeMetadata GetMetadata()
        {
            return new PodcastEpisodeMetadata(Id, Title, PublishDate, Description);
        }
    }
}