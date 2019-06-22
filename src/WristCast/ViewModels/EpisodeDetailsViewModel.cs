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
        private readonly IStorageProvider _storageProvider;

        public EpisodeDetailsViewModel(INavigationService navigationService, DownloadManager downloadService, IStorageProvider storageProvider)
        {
            _navigationService = navigationService;
            _downloadService = downloadService;
            _storageProvider = storageProvider;
            DownloadEpisodeCommand = new Command<PodcastEpisode>(DownloadEpisode);
            PlayEpisode = new Command(async () => await PlayAudio());
        }

        public bool CanDownload => !IsDownloaded;

        public Color DownloadColor => IsDownloaded ? Color.DeepSkyBlue : Color.White;

        public ICommand DownloadEpisodeCommand { get; }

        public bool IsDownloaded { get; private set; }

        public ICommand PlayEpisode { get; }

        public PodcastEpisode PodcastEpisode { get; private set; }

        public void DownloadEpisode(PodcastEpisode episode)
        {
            IsDownloaded = false;
            var filePath = Path.Combine(_storageProvider.MediaFolderPath, $"{episode.Id}.mp3");
            var download = new Download(episode.Audio, filePath);
            _downloadService.AddDownload(download);
            download.StateChanged += OnDownloadStateChanged;
        }

        public override void Prepare(PodcastEpisode parameter)
        {
            PodcastEpisode = parameter;
            IsDownloaded = File.Exists(Path.Combine(_storageProvider.MediaFolderPath, parameter.Id + ".mp3"));
        }

        private void OnDownloadStateChanged(object sender, DownloadStateChangedEventArgs e)
        {
            if (!(sender is Download download)) return;
            if (download.Source == PodcastEpisode.Audio && e.NewState == DownloadState.Completed)
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