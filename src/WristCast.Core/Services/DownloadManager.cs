using System;
using System.Collections.Generic;
using System.Linq;
using WristCast.Core.Model;

namespace WristCast.Core.Services
{
    public class DownloadManager
    {
        private readonly ILog _log;
        private readonly int _downloadLimit;

        public DownloadManager(ILog log, int downloadLimit = 5)
        {
            _log = log;
            _downloadLimit = downloadLimit;
            _downloads = new Dictionary<Guid, Download>();
        }

        private readonly Dictionary<Guid, Download> _downloads;

        public IEnumerable<Download> Downloads => _downloads.Values;

        public void AddDownload(Download download)
        {
            download.StateChanged += Download_StateChanged;
            _downloads.Add(download.Id, download);
            _log.Info($"Added Download {download.Id} from {download.Source}");
            if (_downloads.Count(x => x.Value.State == DownloadState.Downloading) < _downloadLimit)
            {
                download.Execute().ConfigureAwait(false);
            }
        }

        private void Download_StateChanged(object sender, DownloadStateChangedEventArgs e)
        {
            Download download = sender as Download ?? throw new ArgumentException(nameof(sender));
            if (e.OldState == DownloadState.Downloading)
            {
                ExecutePendingDownload();
            }
            _log.Info($"Download {download.Id} state changed from {e.OldState} to {e.NewState}");
            switch (e.NewState)
            {
                case DownloadState.Pending:
                    _log.Info($"Download {download.Id} started");
                    break;
                case DownloadState.Cancelled:
                    _log.Info($"Download {download.Id} started");
                    break;
                case DownloadState.Downloading:
                    _log.Info($"Download {download.Id} started");
                    break;
                case DownloadState.Completed:
                    DownloadCompleted?.Invoke(this, download);
                    _log.Info($"Download {download.Id} completed");
                    break;
                case DownloadState.Error:
                    _log.Info($"Download {download.Id} started");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        public event EventHandler<Download> DownloadCompleted;

        private void ExecutePendingDownload()
        {
            if (_downloads.Any(x => x.Value.State == DownloadState.Pending))
            {
                _downloads.First(x => x.Value.State == DownloadState.Pending).Value.Execute().Start();
            }
        }

        public void RemoveDownload(Guid id)
        {
            _downloads.Remove(id);
        }

        public void CancelDownload(Guid id)
        {
            _downloads[id].Cancel();
        }

        protected virtual async void OnDownloadCompleted(Download e)
        {
            await PodcastManager.Current.AddDownloadedEpisode(e.Source.GetMetadata());
            DownloadCompleted?.Invoke(this, e);
        }
    }
}