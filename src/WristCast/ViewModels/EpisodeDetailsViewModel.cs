using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using WristCast.Core;
using WristCast.Core.Model;
using WristCast.Core.Services;
using Xamarin.Forms;

namespace WristCast.ViewModels
{
    public class EpisodeDetailsViewModel : ViewModel<PodcastEpisode>
    {
        private readonly DownloadManager _downloadService;
        private readonly INavigationService _navigationService;

        public EpisodeDetailsViewModel(INavigationService navigationService, DownloadManager downloadService)
        {
            _navigationService = navigationService;
            _downloadService = downloadService;
            DownloadEpisodeCommand = new Command<PodcastEpisode>(DownloadEpisode);
            PlayEpisode = new Command(async () => await PlayAudio());
        }

        public bool CanDownload => !IsDownloaded;

        public Color DownloadColor => IsDownloaded ? Color.DeepSkyBlue : Color.White;

        public ICommand DownloadEpisodeCommand { get; }

        private bool _isDownloaded;

        public bool IsDownloaded
        {
            get => _isDownloaded;
            private set => SetProperty(ref _isDownloaded,value,
                nameof(IsDownloaded), nameof(CanDownload),nameof(DownloadColor));
        }

        public ICommand PlayEpisode { get; }

        private PodcastEpisode _podcastEpisode;

        public PodcastEpisode PodcastEpisode
        {
            get => _podcastEpisode;
            private set => SetProperty(ref _podcastEpisode,value);
        }

        public void DownloadEpisode(PodcastEpisode episode)
        {
            IsDownloaded = false;
            var filePath = Path.Combine(StorageProvider.Current.MediaFolderPath, $"{episode.Id}.mp3");
            var download = new Download(episode, filePath);
            _downloadService.AddDownload(download);
            download.StateChanged += OnDownloadStateChanged;
        }

        public override void Prepare(PodcastEpisode parameter)
        {
            PodcastEpisode = parameter;
            IsDownloaded = File.Exists(Path.Combine(StorageProvider.Current.MediaFolderPath, parameter.Id + ".mp3"));
        }

        private void OnDownloadStateChanged(object sender, DownloadStateChangedEventArgs e)
        {
            if (!(sender is Download download)) return;
            if (download.Source == PodcastEpisode && e.NewState == DownloadState.Completed)
            {
                IsDownloaded = true;
            }
        }

        private Task PlayAudio()
        {
            return _navigationService.PushAsync<MediaPlayerViewModel, PodcastEpisode>(PodcastEpisode);
        }
    }
}