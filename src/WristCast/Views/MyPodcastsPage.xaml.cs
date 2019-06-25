using WristCast.Core;
using WristCast.Core.Model;
using WristCast.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristCast.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyPodcastsPage : CircleView<MyPodcastViewModel>
    {
        public MyPodcastsPage()
        {
            InitializeComponent();
        }

        private void OnListItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (!(e.Item is PodcastMetadata podcastMetadata)) return;
            ViewModel.LoadPodcastDetailsCommand.Execute(podcastMetadata);
        }
    }
}