using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using WristCast.Core.Data.Repositories;
using WristCast.Core.Model;
using WristCast.Core.Services;
using Xamarin.Forms;

namespace WristCast.ViewModels
{
    public class MyPodcastViewModel : ViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IPodcastMetadaRepository _podcastMetadaRepository;
        private readonly ISearchService _searchService;

        public MyPodcastViewModel(INavigationService navigationService, IPodcastMetadaRepository podcastMetadaRepository,
            ISearchService searchService) : base()
        {
            _navigationService = navigationService;
            _podcastMetadaRepository = podcastMetadaRepository;
            _searchService = searchService;
            LoadPodcastDetailsCommand = new Command<PodcastMetadata>(async metadata => await LoadPodcastDetails(metadata));
        }

        private async Task LoadPodcastDetails(PodcastMetadata metadata)
        {
            try
            {
                var podcast = await _searchService.SearchPodcastAsync(metadata.Id);
                if (podcast == null)
                {
                    CreateTextPopup("Ups. This podcast appears to be no more available...", null, null,
                        true).Show();
                    return;
                }
                await _navigationService.PushAsync<PodcastDetailsViewModel, Podcast>(podcast);
            }
            catch (Exception e)
            {
                CreateTextPopup("Error retrieving podcast details").Show();
            }
        }

        public ICommand LoadPodcastDetailsCommand { get; set; }

        public ObservableCollection<PodcastMetadata> PodcastMetadas { get; set; }

        public override async Task Init()
        {
            PodcastMetadas = new ObservableCollection<PodcastMetadata>(await _podcastMetadaRepository.GetAll());
        }
    }
}