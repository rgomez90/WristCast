using System.Windows.Input;
using WristCast.Core;
using WristCast.Core.Data.Repositories;
using WristCast.Core.Model;
using WristCast.Core.Services;
using Xamarin.Forms;

namespace WristCast.ViewModels
{
    public class PodcastDetailsViewModel : ViewModel<Podcast>
    {
        private readonly ILog _log;
        private readonly IPodcastMetadaRepository _podcastMetadaRepository;
        private readonly INavigationService _navigationService;
        private readonly ISearchService _searchService;

        public PodcastDetailsViewModel(INavigationService navigationService, ISearchService searchService,
            ILog log, IPodcastMetadaRepository podcastMetadaRepository)
        {
            _navigationService = navigationService;
            _searchService = searchService;
            _log = log;
            _podcastMetadaRepository = podcastMetadaRepository;
            RefreshCommand = new Command(Refresh);
            LoadEpisodesCommand = new Command(LoadEpisodes);
            SubscribeToPodcastCommand = new Command(SubscribeToPodcast);
            UnSubscribeToPodcastCommand = new Command(UnSubscribeToPodcast);
        }

        private void UnSubscribeToPodcast(object obj)
        {
            _podcastMetadaRepository.Remove(Podcast.GetMetadata());
        }

        public ICommand UnSubscribeToPodcastCommand { get; set; }

        private void SubscribeToPodcast()
        {
            _podcastMetadaRepository.Add(Podcast.GetMetadata());
        }

        public ICommand LoadEpisodesCommand { get; set; }

        public ICommand SubscribeToPodcastCommand { get; set; }

        public Podcast Podcast { get; private set; }

        public ICommand RefreshCommand{get; }

        public override void Prepare(Podcast parameter)
        {
            Podcast = parameter;
        }

        public async void ShowEpisodeDetails(PodcastEpisode episode)
        {
            await _navigationService.PushAsync<EpisodeDetailsViewModel, PodcastEpisode>(episode);
        }

        private void LoadEpisodes()
        {
        }

        private void Refresh(object obj)
        {
            _log.Info("Refreshing");
        }
    }
}
