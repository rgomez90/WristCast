using WristCast.Core.Services;
using WristCast.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristCast.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : CircleView<SearchViewModel>
	{
		public SearchPage ()
		{
			InitializeComponent ();
		}
        
        private async void Cell_OnTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;
            var selectedItem = e.Item as ISearchResult;
            await ViewModel.ShowDetails(selectedItem);
        }
    }
}