using System.Windows.Input;
using WristCast.Services;
using Xamarin.Forms;

namespace WristCast.ViewModels
{
    public class VolumeViewModel : ViewModel
    {
        public VolumeViewModel()
        {
            VolumeCommand = new Command(VolumeMute);
        }

        private void VolumeMute()
        {
            IsMuted = !IsMuted;
        }

        public string VolumeIcon => IsMuted ? Utils.FontAwesomeIcons.VolumeMute : Utils.FontAwesomeIcons.VolumeUp;

        public bool IsMuted
        {
            get => AudioPlayer.Current.Muted;
            set
            {
                AudioPlayer.Current.Muted = value; 
                OnPropertyChanged();
                OnPropertyChanged(nameof(VolumeIcon));
            }
        }

        public int Volume
        {
            get => AudioPlayer.Current.Volume;
            set
            {
                AudioPlayer.Current.Volume = value; 
                OnPropertyChanged();
            }
        }

        public ICommand VolumeCommand { get; set; }
    }
}