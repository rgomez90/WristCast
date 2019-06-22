using System.Windows.Input;
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

        public string VolumeIcon
        {
            get
            {
                if (IsMuted) return Utils.FontAwesomeIcons.VolumeMute;
                return Utils.FontAwesomeIcons.VolumeUp;
            }
        }

        public bool IsMuted
        {
            get => AudioPlayer.Current.Muted;
            set => AudioPlayer.Current.Muted = value;
        }

        public int Volume
        {
            get => AudioPlayer.Current.Volume;
            set => AudioPlayer.Current.Volume = value;
        }

        public ICommand VolumeCommand { get; set; }
    }
}