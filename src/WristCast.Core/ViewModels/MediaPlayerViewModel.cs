using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MediaManager;
using MediaManager.Media;
using MediaManager.Playback;
using Xamarin.Forms;

namespace WristCast.Core.ViewModels
{
    public class MediaPlayerViewModel : ViewModel
    {
        public MediaPlayerViewModel()
        {
            PlayOrStopCommand = new Command(async () => await PlayOrStop());
            StopCommand = new Command(async () => await Stop());
            NextCommand = new Command(async () => await Next());
            PreviousCommand = new Command(async () => await Previous());
            BackButtonImage = ImageSource.FromFile("10_sec_backward.png");
            PauseButtonImage = ImageSource.FromFile("pause.png");
            PlayButtonImage = ImageSource.FromFile("play.png");
            StopButtonImage = ImageSource.FromFile("stop.png");
            ForwardButtonImage = ImageSource.FromFile("10_sec_forward.png");
            MoveToCommand = new Command(MoveTo);
            MoveToStartedCommand = new Command(MoveToStarted);
            CrossMediaManager.Current.MediaItemChanged += OnMediaItemChanged;
            CrossMediaManager.Current.PositionChanged += OnPositionChanged;
        }

        private void MoveToStarted()
        {
            CrossMediaManager.Current.PositionChanged -= OnPositionChanged;
        }

        private void MoveTo()
        {
            CrossMediaManager.Current.SeekTo(TimeSpan.FromSeconds(ActualSecond));
            CrossMediaManager.Current.PositionChanged += OnPositionChanged;
        }

        private void OnPositionChanged(object sender, PositionChangedEventArgs e)
        {
            ActualSecond = e.Position.TotalSeconds;
        }

        private void OnMediaItemChanged(object sender, MediaItemEventArgs e)
        {
            ActualTitle = CrossMediaManager.Current.MediaQueue.Current.DisplayTitle;
            TotalSeconds = CrossMediaManager.Current.MediaQueue.Current.Duration.TotalSeconds;
        }

        public double TotalSeconds { get; set; }

        public ImageSource StopButtonImage { get; set; }

        public ImageSource ForwardButtonImage { get; set; }

        public ImageSource PlayButtonImage { get; set; }

        public ImageSource PauseButtonImage { get; set; }

        public string ActualTitle { get; private set; }

        private async Task Previous()
        {
            await CrossMediaManager.Current.PlayPreviousOrSeekToStart();
        }

        private async Task Next()
        {
            if (CrossMediaManager.Current.MediaQueue.HasNext())
            {
                await CrossMediaManager.Current.PlayNext();
            }
            else
            {
                await CrossMediaManager.Current.Play(CrossMediaManager.Current.MediaQueue.First());
            }
        }

        public ICommand NextCommand { get; set; }

        public ICommand PreviousCommand { get; set; }

        public ICommand StopCommand { get; set; }

        private async Task Stop()
        {
            await CrossMediaManager.Current.Stop();
        }

        public ICommand PlayOrStopCommand { get; }

        public ImageSource BackButtonImage { get; }

        public double ActualSecond { get; private set; }

        public ICommand MoveToStartedCommand { get; }

        public ICommand MoveToCommand { get; }

        public async Task PlayOrStop()
        {
            if (CrossMediaManager.Current.IsPlaying())
            {
                await CrossMediaManager.Current.Pause();
            }
            else
            {
                await CrossMediaManager.Current.Play();
            }
        }
    }
}