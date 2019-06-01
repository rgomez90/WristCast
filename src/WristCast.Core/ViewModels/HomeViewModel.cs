using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WristCast.Core.ViewModels
{
    public class HomeViewModel : ViewModel
    {
        private readonly INavigationService _navigationService;
        private bool _isFirstUse;

        public HomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Search = new Command(async ()=>await _navigationService.PushModalAsync<SearchViewModel>());
            MyPodcasts= new Command(async ()=> await _navigationService.PushModalAsync<MyPodcastViewModel>());
        }

        public Command Search { get; }

        public Command MyPodcasts{ get; }

        public override async Task Init()
        {
            _isFirstUse = Secrets.ApiKey == null;
            if (_isFirstUse)
            {
                await _navigationService.PushModalAsync<FirstUseViewModel>();
            }
        }
    }
}
