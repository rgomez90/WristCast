using WristCast.ViewModels;
using Xamarin.Forms.Xaml;

namespace WristCast.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MediaPlayerPage : CircleView<MediaPlayerViewModel>
	{
		public MediaPlayerPage ()
		{
			InitializeComponent ();
		}
	}
}