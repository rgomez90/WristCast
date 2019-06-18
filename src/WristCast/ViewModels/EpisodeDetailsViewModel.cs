using System.IO;
using System.Windows.Input;
using WristCast.Core;
using WristCast.Core.Model;
using WristCast.Core.Services;
using Xamarin.Forms;

namespace WristCast.ViewModels
{
    public class EpisodeDetailsViewModel : ViewModel<PodcastEpisode>
    {
        private readonly INavigationService _navigationService;
        private readonly DownloadManager _downloadService;
        private readonly IStorageProvider _storageProvider;
        public PodcastEpisode PodcastEpisode { get; private set; }

        public bool IsDownloaded { get; private set; }

        public EpisodeDetailsViewModel(INavigationService navigationService, DownloadManager downloadService, IStorageProvider storageProvider)
        {
            _navigationService = navigationService;
            _downloadService = downloadService;
            _storageProvider = storageProvider;
            DownloadEpisodeCommand = new Command<PodcastEpisode>(DownloadEpisode);
            PlayEpisode = new Command(PlayAudio);
        }

        private void PlayAudio()
        {
            string source = "";
            if (PodcastEpisode.IsDownloaded)
            {
                source = Path.Combine(_storageProvider.MediaFolderPath, PodcastEpisode.Id + ".mp3");
            }
            else
            {
                source = PodcastEpisode.Audio;
            }
            _navigationService.PushModalAsync<MediaPlayerViewModel, PodcastEpisode>(PodcastEpisode);
        }

        public void DownloadEpisode(PodcastEpisode episode)
        {
            IsDownloaded = false;
            var filePath = Path.Combine(_storageProvider.MediaFolderPath, $"{episode.Id}.mp3");
            var download = new Download(episode.Audio, filePath);
            _downloadService.AddDownload(download);
            download.StateChanged += OnDownloadStateChanged;
        }

        private void OnDownloadStateChanged(object sender, DownloadStateChangedEventArgs e)
        {
            if (!(sender is Download download)) return;
            if (download.Source == PodcastEpisode.Audio && e.NewState == DownloadState.Completed)
            {
                IsDownloaded = true;
            }
        }

        public ICommand DownloadEpisodeCommand { get; }

        public ICommand PlayEpisode { get; }

        public bool CanDownload => !IsDownloaded;

        public override void Prepare(PodcastEpisode parameter)
        {
            PodcastEpisode = parameter;
            IsDownloaded = File.Exists(Path.Combine(_storageProvider.MediaFolderPath, parameter.Id + ".mp3"));
        }
    }
}