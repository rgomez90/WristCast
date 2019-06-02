using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.Applications;
using WristCast.Core.ViewModels;
using Xamarin.Forms;
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