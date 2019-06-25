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
            SearchCommand = new Command(async () => await _navigationService.PushAsync<SearchViewModel>());
            MyPodcastsCommand = new Command(async () => await _navigationService.PushAsync<MyPodcastViewModel>());
            MyDownloadsCommand = new Command(async () => await _navigationService.PushAsync<DownloadsViewModel>());
            MediaPlayerCommand = new Command(async () => await _navigationService.PushAsync<MediaPlayerViewModel>());
        }

        private string _apiKey;

        public string ApiKey
        {
            get => _apiKey;
            private set =>SetProperty(ref _apiKey, value);
        }

        public Command SearchCommand { get; }

        public Command MyPodcastsCommand { get; }

        public Command MyDownloadsCommand { get; }

        public Command MediaPlayerCommand { get; }

        public override async Task Init()
        {
            try
            {
                ApiKey = Secrets.ApiKey;
                _isFirstUse = ApiKey == null;
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
