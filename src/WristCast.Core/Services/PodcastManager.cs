using System.Collections.Generic;
using WristCast.Core.Model;

namespace WristCast.Core.Services
{
    public sealed class PodcastManager
    {
        private static Dictionary<string, Podcast> _subscribedPodcasts;
        private static Dictionary<string, PodcastEpisode> _savedEpisodes;
        public static Dictionary<string, Podcast> SubscribedPodcasts => _subscribedPodcasts;
        public static Dictionary<string, PodcastEpisode> SavedEpisodes => _savedEpisodes;
    }
}
