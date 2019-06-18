using Xamarin.Forms.Xaml;

namespace WristCast.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AudioPlayerPage : CircleView<AudioPlayerViewModel>
	{
		public AudioPlayerPage ()
		{
			InitializeComponent ();
		}
	}
}