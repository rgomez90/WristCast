using WristCast.Core.Model;
using WristCast.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristCast.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PodcastDetailsPage : CircleView<PodcastDetailsViewModel>
	{
		public PodcastDetailsPage ()
		{
			InitializeComponent ();
		}

        private void Cell_OnTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var selectedItem = e.Item as PodcastEpisode;
            ViewModel.ShowEpisodeDetails(selectedItem);
        }
    }
}