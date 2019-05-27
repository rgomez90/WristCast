using Xamarin.Forms;

namespace WristCast.Core.ViewModels
{
    public class HomeViewModel : ViewModel
    {
        public HomeViewModel()
        {
        }

        public Command Search { get; }

        public Command MyPodcasts{ get; }

    }
}
