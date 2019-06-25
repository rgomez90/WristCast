using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using WristCast.Core;
using WristCast.Core.Model;
using WristCast.Core.Services;
using Xamarin.Forms;

namespace WristCast.ViewModels
{
    public class DownloadsViewModel : ViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly ISearchService _searchService;

        public DownloadsViewModel(INavigationService navigationService, ISearchService searchService)
        {
            _navigationService = navigationService;
            _searchService = searchService;
            PlayEpisodeCommand = new Command<PodcastEpisodeMetadata>(async s => await PlayEpisode(s));
            DownloadedEpisodes = new List<PodcastEpisodeMetadata>();
        }

        private async Task PlayEpisode(PodcastEpisodeMetadata podcastEpisodeMetadata)
        {
            var podcast = await _searchService.SearchEpisodeAsync(podcastEpisodeMetadata.Id);
            podcast.IsDownloaded = true;
            await _navigationService.PushAsync<MediaPlayerViewModel, PodcastEpisode>(podcast);
        }

        public override Task Init()
        {
            DownloadedEpisodes = PodcastManager.Current.DownloadedEpisodes;
            return Task.CompletedTask;
        }

        private IReadOnlyCollection<PodcastEpisodeMetadata> _downloadedEpisodes;

        public IReadOnlyCollection<PodcastEpisodeMetadata> DownloadedEpisodes
        {
            get => _downloadedEpisodes;
            set => SetProperty(ref _downloadedEpisodes, value, 
                nameof(DownloadedEpisodes), nameof(AreDownloadsEmpty), nameof(AreDownloadsNotEmpty));
        }

        public bool AreDownloadsEmpty => DownloadedEpisodes.Count > 0;

        public bool AreDownloadsNotEmpty => !AreDownloadsEmpty;

        public ICommand PlayEpisodeCommand { get; set; }
    }
}
