using System;
using ListenNotesSearch.NET.Models;

namespace WristCast.Core.Model
{
    public class PodcastEpisode
    {
        public PodcastEpisode(IEpisode episode)
        {
            Title=episode.Title;
            Description = episode.Description;
            Image = episode.Image;
            Audio=episode.Audio;
            AudioLength=TimeSpan.FromSeconds(episode.AudioLengthSec);
            PublishDate=episode.PubDateMs;
            ExplicitContent=episode.ExplicitContent;
        }

        public int PublishDate { get; set; }

        public TimeSpan AudioLength { get; set; }

        public string Title { get; set; }

        public string Audio { get; set; }

        public string Publisher { get; set; }

        public bool ExplicitContent { get; set; }

        public string Website { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }
    }
}