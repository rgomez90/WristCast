using MvvmCross.Forms.Views;
using WristCast.Core.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WristCast
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : MvxContentPage<HomeViewModel>
    {
        public HomePage()
        {
            InitializeComponent();
        }
    }
}