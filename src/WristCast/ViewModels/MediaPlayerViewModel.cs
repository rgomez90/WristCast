using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Tizen.Multimedia;
using WristCast.Core;
using WristCast.Core.Model;
using Xamarin.Forms;

namespace WristCast.ViewModels
{
    public class MediaPlayerViewModel : ViewModel<PodcastEpisode>
    {
        private readonly IStorageProvider _storageProvider;
        private readonly Player _mediaPlayer;

        public MediaPlayerViewModel(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
            _mediaPlayer = new Player();
            BgImage = new UriImageSource();
            PlayOrStopCommand = new Command(async () => await PlayOrStop());
            StopCommand = new Command(Stop);
            NextCommand = new Command(Next);
            PreviousCommand = new Command(Previous);
            BackButtonImage = ImageSource.FromFile("10_sec_backward.png");
            PauseButtonImage = ImageSource.FromFile("pause.png");
            PlayButtonImage = ImageSource.FromFile("play.png");
            StopButtonImage = ImageSource.FromFile("pause.png");
            ForwardButtonImage = ImageSource.FromFile("10_sec_forward.png");
            MoveToCommand = new Command(MoveTo);
            MoveToStartedCommand = new Command(MoveToStarted);
        }

        public double ActualSecond
        {
            get
            {
                if (_mediaPlayer.State == PlayerState.Idle || _mediaPlayer.State == PlayerState.Preparing)
                    return -1;
                return _mediaPlayer.GetPlayPosition();
            }
        }

        public string ActualTitle { get; private set; }

        public ImageSource BackButtonImage { get; }

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

        public double TotalSeconds => 0;

        public UriImageSource BgImage { get; set; }

        public async Task PlayOrStop()
        {
            switch (_mediaPlayer.State)
            {
                case PlayerState.Idle:
                    await _mediaPlayer.PrepareAsync();
                    _mediaPlayer.Start();
                    break;
                case PlayerState.Ready:
                case PlayerState.Paused:
                    _mediaPlayer.Start();
                    break;
                case PlayerState.Playing:
                    _mediaPlayer.Pause();
                    break;
                case PlayerState.Preparing:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void MoveTo()
        {
        }

        private async void MoveToStarted()
        {
            await _mediaPlayer.SetPlayPositionAsync(-_mediaPlayer.GetPlayPosition(), false);
        }

        private void Next()
        {

        }

        private void Previous()
        {
        }

        private void Stop()
        {
            _mediaPlayer.Stop();
        }

        public override void Prepare(PodcastEpisode parameter)
        {
            var file = Path.Combine(_storageProvider.MediaFolderPath, parameter.Id + ".mp3");
            BgImage = new UriImageSource { Uri = new Uri(parameter.Image) };
            if (parameter.IsDownloaded && File.Exists(file))
            {
                _mediaPlayer.SetSource(new MediaUriSource(file));
            }
            else
            {
                _mediaPlayer.SetSource(new MediaUriSource(parameter.Audio));
            }

        }
    }
}