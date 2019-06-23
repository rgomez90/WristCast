using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SQLite;
using WristCast.Core.Data.Repositories;
using WristCast.Core.IoC;
using WristCast.Core.Model;

namespace WristCast.Core
{
    public class PodcastManager
    {
        private static PodcastManager _current;
        private HashSet<PodcastMetadata> _subscribedPodcasts;
        private HashSet<PodcastEpisodeMetadata> _downloadedEpisodes;

        private IPodcastEpisodeMetadataRepository _podcastEpisodeMetadataRepository;
        private IPodcastMetadaRepository _podcastMetadaRepository;

        public static PodcastManager Current => _current;

        public IReadOnlyCollection<PodcastMetadata> SubscribedPodcasts => _subscribedPodcasts;
        public IReadOnlyCollection<PodcastEpisodeMetadata> DownloadedEpisodes => _downloadedEpisodes;

        public async Task SubscribeToPodcast(Podcast podcast)
        {
            var metadata = podcast.GetMetadata();
            await _podcastMetadaRepository.Add(metadata);
            _subscribedPodcasts.Add(metadata);
        }

        public async Task UnsubscribeToPodcast(Podcast podcast)
        {
            var metadata = podcast.GetMetadata();
            await _podcastMetadaRepository.Remove(podcast.GetMetadata());
            _subscribedPodcasts.Remove(metadata);
        }

        public async Task AddDownloadedEpisode(PodcastEpisodeMetadata episode)
        {
            await _podcastEpisodeMetadataRepository.Add(episode);
            _downloadedEpisodes.Add(episode);
        }

        public async Task RemoveDownloadedEpisode(PodcastEpisodeMetadata episode)
        {
            await _podcastEpisodeMetadataRepository.Remove(episode);
            _downloadedEpisodes.Remove(episode);
        }

        public static async Task Init()
        {
            var podcastRepo = IocContainer.Instance.Resolve<IPodcastMetadaRepository>();
            var episodesRepo = IocContainer.Instance.Resolve<IPodcastEpisodeMetadataRepository>();
            var podcasts = await podcastRepo.GetAll();
            var episodes = await episodesRepo.GetAll();
            _current = new PodcastManager
            {
                _podcastMetadaRepository = podcastRepo,
                _subscribedPodcasts = new HashSet<PodcastMetadata>(podcasts),
                _podcastEpisodeMetadataRepository = episodesRepo,
                _downloadedEpisodes = new HashSet<PodcastEpisodeMetadata>(episodes)
            };
        }
    }
}
