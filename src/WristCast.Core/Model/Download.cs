using System;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using WristCast.Core.IoC;
using WristCast.Core.Services;

namespace WristCast.Core.Model
{
    public class Download
    {
        private CancellationTokenSource _cts;
        private readonly Progress<long> _progress;

        public Download(PodcastEpisode podcastEpisode, string filePath)
        {
            Id = new Guid();
            Source = podcastEpisode;
            FilePath = filePath;
            State = DownloadState.Pending;
            ErrorMessage = null;
            _progress = new Progress<long>();
            _progress.ProgressChanged += OnProgressChanged;
            _cts = new CancellationTokenSource();
        }

        private void OnProgressChanged(object sender, long e)
        {
            Downloaded = e;
            ProgressChanged?.Invoke(this, e);
        }

        public long? Size { get; private set; }

        public long Downloaded { get; private set; }
        
        public double DownloadedPercentage => Size.HasValue || Size == 0 ? (Downloaded / Size.Value) * 100d : 0d;

        public Guid Id { get; }

        public PodcastEpisode Source { get; }

        public string FilePath { get; }

        public DownloadState State { get; private set; }

        public string ErrorMessage { get; private set; }

        public event EventHandler<long> ProgressChanged;

        public void Cancel()
        {
            if (State == DownloadState.Downloading)
            {
                _cts.Cancel();
            }
        }

        public event EventHandler<DownloadStateChangedEventArgs> StateChanged;

        public event EventHandler DownloadMetadataReady;

        private void SetState(DownloadState state)
        {
            if (state != State)
            {
                var args = new DownloadStateChangedEventArgs(State, state);
                State = state;
                StateChanged?.Invoke(this, args);
            }
        }

        public async Task Execute()
        {
            using (var con = IocContainer.Instance.BeginLifetimeScope())
            {
                var service = con.Resolve<IDownloadService>();
                service.DownloadContentHeaderReady += OnDownloadContentHeaderReady;
                try
                {
                    SetState(DownloadState.Downloading);
                    await service.DownloadFileAsync(Source.Audio, FilePath, _cts.Token, _progress);
                    SetState(DownloadState.Completed);
                }
                catch (TaskCanceledException)
                {
                    SetState(DownloadState.Cancelled);

                }
                catch (Exception e)
                {
                    ErrorMessage = e.Message;
                    SetState(DownloadState.Error);
                }
                finally
                {
                    _cts = new CancellationTokenSource();
                }
            }
        }

        private void OnDownloadContentHeaderReady(object sender, HttpContentHeaders e)
        {
            Size = e.ContentLength;
            DownloadMetadataReady?.Invoke(this, EventArgs.Empty);
        }
    }
}