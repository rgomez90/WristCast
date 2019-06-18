using System.Windows.Input;
using WristCast.Core;
using WristCast.Core.Model;
using WristCast.Core.Services;
using Xamarin.Forms;

namespace WristCast.ViewModels
{
    public class PodcastDetailsViewModel : ViewModel<Podcast>
    {
        private readonly INavigationService _navigationService;
        private readonly ISearchService _searchService;
        private readonly ILog _log;
        public Podcast Podcast { get; private set; }

        public ICommand RefreshCommand{get; }

        public PodcastDetailsViewModel(INavigationService navigationService, ISearchService searchService, ILog log)
        {
            _navigationService = navigationService;
            _searchService = searchService;
            _log = log;
            RefreshCommand=new Command(Refresh);
            LoadEpisodesCommand = new Command(LoadEpisodes);
        }

        private void LoadEpisodes()
        {
           //
        }

        public ICommand LoadEpisodesCommand { get; set; }

        private void Refresh(object obj)
        {
            _log.Info("Refreshind");
        }

        public async void ShowEpisodeDetails(PodcastEpisode episode)
        {
            await _navigationService.PushModalAsync<EpisodeDetailsViewModel, PodcastEpisode>(episode);
        }

        public override void Prepare(Podcast parameter)
        {
            Podcast = parameter;
        }
    }
}
