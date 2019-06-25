using System.Threading.Tasks;
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
            SubscribeToPodcastCommand = new Command(async () => await SubscribeToPodcast());
            UnSubscribeToPodcastCommand = new Command(async () => await UnSubscribeToPodcast());
        }

        private Task UnSubscribeToPodcast()
        {
            return PodcastManager.Current.UnsubscribeToPodcast(Podcast);
        }

        public ICommand UnSubscribeToPodcastCommand { get; set; }

        private Task SubscribeToPodcast()
        {
            return PodcastManager.Current.SubscribeToPodcast(Podcast);
        }

        public ICommand LoadEpisodesCommand { get; set; }

        public ICommand SubscribeToPodcastCommand { get; set; }

        private Podcast _podcast;

        public Podcast Podcast
        {
            get => _podcast;
            private set => SetProperty(ref _podcast, value);
        }

        public ICommand RefreshCommand { get; }

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
