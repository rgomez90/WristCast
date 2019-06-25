using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WristCast.Core;
using WristCast.Core.Data.Repositories;
using WristCast.Core.Model;
using WristCast.Core.Services;
using Xamarin.Forms;

namespace WristCast.ViewModels
{
    public class MyPodcastViewModel : ViewModel
    {
        private readonly ILog _log;
        private readonly INavigationService _navigationService;
        private readonly IPodcastMetadaRepository _podcastMetadaRepository;
        private readonly ISearchService _searchService;

        public MyPodcastViewModel(INavigationService navigationService, ILog log, IPodcastMetadaRepository podcastMetadaRepository,
            ISearchService searchService)
        {
            _navigationService = navigationService;
            _log = log;
            _podcastMetadaRepository = podcastMetadaRepository;
            _searchService = searchService;
            LoadPodcastDetailsCommand = new Command<PodcastMetadata>(async metadata => await LoadPodcastDetails(metadata));
        }

        public ICommand LoadPodcastDetailsCommand { get; set; }

        private ObservableCollection<PodcastMetadata> _podcastMetadas;

        public ObservableCollection<PodcastMetadata> PodcastMetadas
        {
            get => _podcastMetadas;
            set => SetProperty(ref _podcastMetadas, value);
        }

        public override Task Init()
        {
            PodcastMetadas = new ObservableCollection<PodcastMetadata>(PodcastManager.Current.SubscribedPodcasts);
            return Task.CompletedTask;
        }

        private async Task LoadPodcastDetails(PodcastMetadata metadata)
        {
            var popUp = CreateProgressPopup("Retrieving podcast details");
            popUp.Show();
            try
            {
                var podcast = await _searchService.SearchPodcastAsync(metadata.Id);
                if (podcast == null)
                {
                    CreateTextPopup("Ups. This podcast appears to be no more available...").Show();
                }
                else
                {
                    await _navigationService.PushAsync<PodcastDetailsViewModel, Podcast>(podcast);
                }
            }
            catch (Exception e)
            {
                const string error = "Error retrieving podcast details";
                _log.Error(error, e);
                CreateTextPopup(error).Show();
            }
            finally
            {
                popUp.Dismiss();
            }
        }
    }
}