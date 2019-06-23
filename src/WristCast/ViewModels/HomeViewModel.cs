using System;
using System.Threading.Tasks;
using Autofac;
using WristCast.Core;
using WristCast.Core.IoC;
using WristCast.Core.Model;
using WristCast.Core.Services;
using Xamarin.Forms;

namespace WristCast.ViewModels
{
    public class HomeViewModel : ViewModel
    {
        private readonly INavigationService _navigationService;
        private bool _isFirstUse;

        public HomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Search = new Command(async () => await _navigationService.PushAsync<SearchViewModel>());
            MyPodcasts = new Command(async () => await _navigationService.PushAsync<MyPodcastViewModel>());
            Test = new Command(async () => await TestMeth());
        }

        private async Task TestMeth()
        {
            using (var con = IocContainer.Instance.BeginLifetimeScope())
            {
                var s = con.Resolve<ISearchService>();
                var ep = await s.SearchEpisodeAsync("727ff294ace34c1884ce01d1ad2ad279");
                await _navigationService.PushAsync<MediaPlayerViewModel, PodcastEpisode>(ep);
            }
        }

        public Command Search { get; }

        public Command Test { get; }

        public Command MyPodcasts { get; }

        public override async Task Init()
        {
            try
            {
                _isFirstUse = Secrets.ApiKey == null;
                if (_isFirstUse)
                {
                    await _navigationService.PushModalAsync<FirstUseViewModel>();
                }
            }
            catch (Exception)
            {
                ShowToast("Error reading ApiKey.\nPlease check ApiKey settings.");
                throw;
            }
        }
    }
}
