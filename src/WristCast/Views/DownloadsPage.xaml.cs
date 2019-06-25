using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WristCast.Core;
using WristCast.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristCast.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DownloadsPage : CircleView<DownloadsViewModel>
	{
		public DownloadsPage ()
		{
			InitializeComponent ();
		}

        private void OnListItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.PlayEpisodeCommand.Execute(null);
        }
    }
}