using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Tizen.Multimedia;
using WristCast.Core;
using WristCast.Core.Model;
using WristCast.Core.Services;
using Xamarin.Forms;

namespace WristCast.ViewModels
{
    public class MediaPlayerViewModel : ViewModel<PodcastEpisode>
    {
        private readonly INavigationService _navigationService;
        private readonly IStorageProvider _storageProvider;
        private PodcastEpisode _parameter;

        public MediaPlayerViewModel(IStorageProvider storageProvider, INavigationService navigationService)
        {
            _storageProvider = storageProvider;
            _navigationService = navigationService;
            PlayOrStopCommand = new Command(PlayOrStop);
            StopCommand = new Command(Stop);
            NextCommand = new Command(Next);
            PreviousCommand = new Command(Previous);
            VolumeCommand = new Command(async () => await navigationService.PushModalAsync<VolumeViewModel>());
            BackButtonImage = ImageSource.FromFile("10_sec_backward.png");
            PauseButtonImage = ImageSource.FromFile("pause.png");
            PlayButtonImage = ImageSource.FromFile("play.png");
            StopButtonImage = ImageSource.FromFile("pause.png");
            ForwardButtonImage = ImageSource.FromFile("10_sec_forward.png");
            MoveToCommand = new Command(async () => await MoveTo());
            MoveToStartedCommand = new Command(MoveToStarted);
            AddEventHandlers();
        }

        public string ActualPosition => AudioPlayer.Current.Position.ToString("g");

        public int ActualSecond => (int)AudioPlayer.Current.Position.TotalSeconds;

        public string ActualTitle => CurrentEpisode?.Title ?? string.Empty;

        public ImageSource BackButtonImage { get; }

        public ImageSource BgImage { get; set; }

        public ImageSource ForwardButtonImage { get; set; }

        public ICommand MoveToCommand { get; }

        public ICommand MoveToStartedCommand { get; }

        public ICommand NextCommand { get; set; }

        public ImageSource PauseButtonImage { get; set; }

        public ImageSource PlayButtonImage { get; set; }

        public ICommand PlayOrStopCommand { get; }

        public ICommand PreviousCommand { get; set; }

        public ImageSource StopButtonImage { get; set; }

        public ICommand StopCommand { get; set; }

        public double TotalSeconds
        {
            get => AudioPlayer.Current.Duration.TotalSeconds == 0 ? 1 : AudioPlayer.Current.Duration.TotalSeconds;
            //{
            //    if (AudioPlayer.Current.MediaPlayer.State == PlayerState.Preparing
            //        || AudioPlayer.Current.MediaPlayer.State == PlayerState.Idle)
            //        return 0;
            //    return AudioPlayer.Current.MediaPlayer.StreamInfo.GetDuration();
            //}
        }

        public ICommand VolumeCommand { get; set; }

        private static PodcastEpisode CurrentEpisode { get; set; }

        public override Task Clean()
        {
            RemoveEventHandlers();
            return Task.CompletedTask;
        }

        public override async Task Init()
        {
            if (_parameter != null)
            {
                if (CurrentEpisode != null && CurrentEpisode.Id == _parameter.Id) return;
                await ChangeSource(_parameter);
                AudioPlayer.Current.Play();
            }
        }

        public void PlayOrStop()
        {
            switch (AudioPlayer.Current.State)
            {
                case PlayerState.Ready:
                case PlayerState.Paused:
                    AudioPlayer.Current.Play();
                    break;
                case PlayerState.Playing:
                    AudioPlayer.Current.Pause();
                    break;
                case PlayerState.Preparing:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void Prepare(PodcastEpisode parameter)
        {
            _parameter = parameter;
        }

        private void AddEventHandlers()
        {
            AudioPlayer.Current.PlayPositionChanged += OnPlayPositionChanged;
            AudioPlayer.Current.MetadataReady += OnMetadataReady;
            AudioPlayer.Current.SourceChanged += OnSourceChanged;
        }

        private async Task ChangeSource(PodcastEpisode source)
        {
            var file = Path.Combine(_storageProvider.MediaFolderPath, source.Id + ".mp3");
            MediaSource mediaSource = source.IsDownloaded && File.Exists(file) ?
                new MediaUriSource(file) :
                new MediaUriSource(source.Audio);
            await AudioPlayer.Current.ChangeSource(mediaSource);
        }

        private Task MoveTo()
        {
            return AudioPlayer.Current.SeekTo(TimeSpan.FromSeconds(ActualSecond));
        }

        private void MoveToStarted()
        {
        }

        private void Next()
        {
        }

        private void OnMetadataReady(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(ActualTitle));
            OnPropertyChanged(nameof(ActualSecond));
            OnPropertyChanged(nameof(TotalSeconds));
            OnPropertyChanged(nameof(ActualPosition));
        }

        private void OnPlayPositionChanged(object sender, TimeSpan e)
        {
            OnPropertyChanged(nameof(ActualSecond));
            OnPropertyChanged(nameof(ActualPosition));
        }

        private void OnSourceChanged(object sender, EventArgs e)
        {
            BgImage = ImageSource.FromUri(new Uri(_parameter.Image));
            CurrentEpisode = _parameter;
            _parameter = null;
        }

        private void Previous()
        {
        }

        private void RemoveEventHandlers()
        {
            AudioPlayer.Current.PlayPositionChanged -= OnPlayPositionChanged;
            AudioPlayer.Current.MetadataReady -= OnMetadataReady;
        }

        private void Stop()
        {
            AudioPlayer.Current.Stop();
        }
    }
}