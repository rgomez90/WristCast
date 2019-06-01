using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WristCast.Core.ViewModels;
using Xamarin.Forms;
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