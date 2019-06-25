using System.Windows.Input;
using WristCast.Core;
using WristCast.Core.Services;
using Xamarin.Forms;

namespace WristCast.ViewModels
{
    public class FirstUseViewModel : ViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly ISecretsService _secretsService;

        public FirstUseViewModel(INavigationService navigationService, ISecretsService secretsService)
        {
            _navigationService = navigationService;
            _secretsService = secretsService;
            ApiKey = string.Empty;
            SaveApiKey = new Command(async () =>
              {
                  await _secretsService.SaveSecretAsync(Secrets.KeyAlias,ApiKey.Trim());
                  IsApiKeySaved = true;
                  await _navigationService.PopModalAsync();
              });
        }

        public bool IsApiKeyValid => !(string.IsNullOrEmpty(ApiKey) || string.IsNullOrWhiteSpace(ApiKey));

        private bool _isApiKeySaved;

        public bool IsApiKeySaved
        {
            get => _isApiKeySaved;
            set => SetProperty(ref _isApiKeySaved,value);
        }

        public string TutorialText => "Your key is: " + ApiKey;

        private string _apiKey;

        public string ApiKey
        {
            get => _apiKey;
            set => SetProperty(ref _apiKey, value, nameof(ApiKey), nameof(IsApiKeyValid));
        }

        public ICommand SaveApiKey { get; }
    }
}