using WristCast.Core;
using WristCast.Core.Model;
using WristCast.Core.Services;
using WristCast.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristCast.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchResultPage : CircleView<SearchResultViewModel>
	{
		public SearchResultPage ()
		{
			InitializeComponent ();
		}

        private async void OnListItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var selectedItem = e.Item as ISearchResult;
            await ViewModel.ShowDetails(selectedItem);
        }
    }
}