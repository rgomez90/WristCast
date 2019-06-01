using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristCast.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AudioPlayerPage : CircleView<AudioPlayerViewModel>
	{
		public AudioPlayerPage ()
		{
			InitializeComponent ();
		}
	}
}