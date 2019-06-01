using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WristCast.Core.Services;
using WristCast.Core.ViewModels;
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