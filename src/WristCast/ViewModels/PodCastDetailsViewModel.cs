using System.Windows.Input;
using WristCast.Core;
using WristCast.Core.Model;
using WristCast.Core.Services;
using Xamarin.Forms;

namespace WristCast.ViewModels
{
    public class PodcastDetailsViewModel : ViewModel<Podcast>
    {
        private readonly ILog _log;
        private readonly INavigationService _navigationService;
        private readonly ISearchService _searchService;

        public PodcastDetailsViewModel(INavigationService navigationService, ISearchService searchService, ILog log)
        {
            _navigationService = navigationService;
            _searchService = searchService;
            _log = log;
            RefreshCommand = new Command(Refresh);
            LoadEpisodesCommand = new Command(LoadEpisodes);
        }

        public ICommand LoadEpisodesCommand { get; set; }

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
