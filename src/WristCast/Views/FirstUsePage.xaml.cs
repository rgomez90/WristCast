using WristCast.Core;
using WristCast.ViewModels;
using Xamarin.Forms.Xaml;

namespace WristCast.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FirstUsePage : CircleView<FirstUseViewModel>
	{
		public FirstUsePage ()
		{
			InitializeComponent ();
		}
	}
}