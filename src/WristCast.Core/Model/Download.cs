using System;
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

        public Download( string source, string filePath)
        {
            Id=new Guid();
            Source = source;
            FilePath = filePath;
            State = DownloadState.Pending;
            ErrorMessage = null;
            _cts = new CancellationTokenSource();
        }

        public Guid Id { get; }
        public string Source { get; }
        public string FilePath { get; }
        public DownloadState State { get; private set; }
        public string ErrorMessage { get; private set; }

        public void Cancel()
        {
            if (State == DownloadState.Downloading)
            {
                _cts.Cancel();
            }
        }

        public event EventHandler<DownloadStateChangedEventArgs> StateChanged;

        private void SetState(DownloadState state)
        {
            if (state!=State)
            {
                var args = new DownloadStateChangedEventArgs(State, state);
                State = state;
                StateChanged?.Invoke(this,args);
            }
        }

        public async Task Execute()
        {
            using (var con = IocContainer.Instance.BeginLifetimeScope())
            {
                var service = con.Resolve<IDownloadService>();
                try
                {
                    SetState(DownloadState.Downloading);
                    await service.DownloadFileAsync(Source,FilePath, _cts.Token);
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
    }
}