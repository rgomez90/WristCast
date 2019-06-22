using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WristCast.Core;
using WristCast.Core.Model;
using WristCast.Core.Services;

namespace WristCast.ViewModels
{
    public class SearchResultViewModel : ViewModel<IEnumerable<ISearchResult>>
    {
        private readonly ILog _log;
        private readonly INavigationService _navigation;
        private readonly ISearchService _searchService;

        public SearchResultViewModel(ISearchService searchService, INavigationService navigationService, ILog log)
        {
            _searchService = searchService;
            _navigation = navigationService;
            _log = log;
        }

        public ObservableCollection<ISearchResult> SearchResults { get; set; } = new ObservableCollection<ISearchResult>();

        public bool Loaded { get; set; } = false;

        public override void Prepare(IEnumerable<ISearchResult> parameter)
        {
            SearchResults = new ObservableCollection<ISearchResult>(parameter);
            Loaded = true;
        }

        public async Task ShowDetails(ISearchResult searchResult)
        {
            var popUp = CreateProgressPopup("Retrieving details...");
            popUp.Show();
            try
            {
                Task task = null;
                switch (searchResult)
                {
                    case PodcastSearchResult res:
                        var podcast = await _searchService.SearchPodcastAsync(res.Id);
                        task = _navigation.PushAsync<PodcastDetailsViewModel, Podcast>(podcast);
                        break;
                    case EpisodeSearchResult res:
                        var episode = await _searchService.SearchEpisodeAsync(res.Id);
                        task = _navigation.PushAsync<EpisodeDetailsViewModel, PodcastEpisode>(episode);
                        break;
                    default:
                        throw new ArgumentException("searchResult type not expected",nameof(searchResult));
                }
                popUp.Dismiss();
                await task;
            }
            catch (Exception e)
            {
                _log.Error($"Error searching for {searchResult.Id}", e);
                ShowToast("Error retrieving details...");
            }
        }
    }
}
