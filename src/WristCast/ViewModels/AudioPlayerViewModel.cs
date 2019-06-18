using WristCast.Core;
using WristCast.Core.Model;
using WristCast.ViewModels;

namespace WristCast
{
    public class AudioPlayerViewModel:ViewModel<PodcastEpisode>
    {
        private readonly INavigationService _navigation;
        private readonly ILog _log;

        public PodcastEpisode Episode { get; private set; }

        public AudioPlayerViewModel(INavigationService navigation, ILog log)
        {
            _navigation = navigation;
            _log = log;
        }

        public override void Prepare(PodcastEpisode parameter)
        {
            Episode = parameter;
        }
    }
}