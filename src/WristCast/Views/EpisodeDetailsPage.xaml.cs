using WristCast.Core;
using WristCast.ViewModels;
using Xamarin.Forms.Xaml;

namespace WristCast.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EpisodeDetailsPage : CircleView<EpisodeDetailsViewModel>
	{
		public EpisodeDetailsPage ()
		{
			InitializeComponent ();
		}
	}
}