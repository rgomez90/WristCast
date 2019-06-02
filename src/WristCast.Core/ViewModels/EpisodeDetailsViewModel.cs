using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using MediaManager;
using MediaManager.Media;
using WristCast.Core.Model;
using WristCast.Core.Services;
using Xamarin.Forms;

namespace WristCast.Core.ViewModels
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
            CrossMediaManager.Current.MediaQueue.Clear();
            CrossMediaManager.Current.MediaQueue.Add(new MediaItem(
                PodcastEpisode.IsDownloaded ? Path.Combine(_storageProvider.MediaFolderPath, PodcastEpisode.Id + ".mp3") : PodcastEpisode.Audio));
            _navigationService.PushModalAsync<MediaPlayerViewModel>();
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
            Download download = sender as Download;
            if (download == null) return;
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