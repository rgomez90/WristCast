using System.Net;
using ListenNotesSearch.NET.Models;
using WristCast.Core.Model;

namespace WristCast.Core.ViewModels
{
    public class PodcastDetailsViewModel : ViewModel<Podcast>
    {
        private readonly INavigationService _navigationService;
        private readonly ILog _log;
        public Podcast Podcast { get; private set; }

        public PodcastDetailsViewModel(INavigationService navigationService, ILog log)
        {
            _navigationService = navigationService;
            _log = log;
        }

        public void ShowEpisodeDetails(PodcastEpisode episode)
        {
            _navigationService.PushModalAsync<EpisodeDetailsViewModel, PodcastEpisode>(episode);
        }

        public override void Prepare(Podcast parameter)
        {
            Podcast = parameter;
        }
    }
}
