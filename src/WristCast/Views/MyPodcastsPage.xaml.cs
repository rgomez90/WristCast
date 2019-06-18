using WristCast.ViewModels;
using Xamarin.Forms.Xaml;

namespace WristCast.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyPodcastsPage : CircleView<MyPodcastViewModel>
	{
		public MyPodcastsPage ()
		{
			InitializeComponent ();
		}
	}
}