using System.Collections.Generic;
using WristCast.Core.Model;
using WristCast.Core.Services;
using Xamarin.Forms;

namespace WristCast.Core.ViewModels
{
    public class SearchPageViewModel : ViewModel
    {
        public INavigationService Navigation { get; }
        private readonly ISearchService _searchService;

        public SearchPageViewModel(INavigationService navigation, ISearchService searchService)
        {
            Navigation = navigation;
            _searchService = searchService;
            Search = new Command<string>(async (s) => Podcasts = await _searchService.SearchPodcastAsync(s));
            PodcastDetails = new Command<Podcast>(async (p) => await Navigation.PushModalAsync<PodcastDetailsViewModel>(p));
        }

        public Command<Podcast> PodcastDetails { get; }

        public IEnumerable<Podcast> Podcasts { get; set; }

        public Command<string> Search { get; }
    }
}
