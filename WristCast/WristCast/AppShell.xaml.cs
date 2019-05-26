using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristCast.Forms
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppShell : Shell
	{
		public AppShell ()
		{
			InitializeComponent ();
		}
	}
}