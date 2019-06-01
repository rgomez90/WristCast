using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using WristCast.Core.IoC;
using WristCast.Core.Model;
using WristCast.Core.ViewModels;

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
